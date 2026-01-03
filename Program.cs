using MVCCourse.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MVCCourse.Data;
using MVCCourse.Models;
using FluentValidation;
using FluentValidation.AspNetCore; // تأكد من وجود هذه المكتبة

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICES SECTION ---

// إضافة الـ Controllers مع تفعيل الـ FluentValidation التلقائي
builder.Services.AddControllersWithViews();

// إضافة FluentValidation (الربط التلقائي)
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

// إعداد قاعدة البيانات (PostgreSQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// إعداد نظام الهوية (Identity)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false; // تسهيلاً لك في البداية
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// إعداد الـ AutoMapper (الطريقة الصحيحة لـ .NET 8)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// إعداد الـ Razor Pages (مطلوب لـ Identity UI إذا كنت تستخدمها)
builder.Services.AddRazorPages();

var app = builder.Build();

// --- 2. MIDDLEWARE PIPELINE (THE ORDER MATTERS!) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // مهم جداً لتحميل ملفات الـ CSS والـ JS

app.UseRouting();

// الترتيب هنا "حياة أو موت" للكود
app.UseAuthentication(); // من أنت؟
app.UseAuthorization();  // ماذا يمكنك أن تفعل؟

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
        // تنفيذ الـ Migrations تلقائياً عند التشغيل
        await context.Database.MigrateAsync(); 

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        
        // استدعاء بيانات البداية (Admins, Employees, Customers)
        await DbInitializer.SeedData(roleManager, userManager);
        
        Console.WriteLine("Database synchronized and seeded successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();