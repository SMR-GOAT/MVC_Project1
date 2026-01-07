using MVCCourse.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MVCCourse.Data;
using MVCCourse.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MVCCourse.Services;


var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICES SECTION ---

// إضافة الـ Controllers مع تفعيل الـ FluentValidation التلقائي
builder.Services.AddControllersWithViews();

// إعداد FluentValidation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

// إعداد قاعدة البيانات (PostgreSQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// إعداد نظام الهوية (Identity) - تم ضبطها لتطابق قيود الـ Validator
builder.Services.AddIdentity<ApplicationUserModel, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true; // ضمان عدم تكرار الإيميل
    
    // إعدادات كلمة المرور (Password Complexity)
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// إعداد الـ AutoMapper (تحديد ملف الـ Profile بدقة)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// تسجيل خدمات التطبيق (Custom Services) - استدعاء واحد فقط
builder.Services.AddApplicationServices();

// إعداد الـ Razor Pages (مطلوب لـ Identity)
builder.Services.AddRazorPages();

var app = builder.Build();

// --- 2. MIDDLEWARE PIPELINE ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // لتحميل ملفات الـ CSS والـ JS

app.UseRouting();

// الترتيب الحساس للأمان
app.UseAuthentication(); // التحقق من الهوية
app.UseAuthorization();  // التحقق من الصلاحيات

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();

// --- 3. DATABASE MIGRATION & SEED DATA ---

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync(); 

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUserModel>>();
        
        // استدعاء بيانات البداية
        await DbInitializer.SeedData(roleManager, userManager);
        
        Console.WriteLine("System is ready and database is synchronized!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during startup.");
    }
}

app.Run();