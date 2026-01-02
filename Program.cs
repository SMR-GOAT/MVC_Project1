using MVCCourse.Validations; // هذا المجلد اللي فيه الـ LoginValidator حقك
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // تعديل جديد: لاستيراد أدوات الحماية
using MVCCourse.Data;
using MVCCourse.Models;
using FluentValidation;




var builder = WebApplication.CreateBuilder(args);

// --- 1. قسم الخدمات (الأدوات) ---

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- (تعديل جديد هنا) ---
// إضافة نظام الهوية (الحارس) وربطه بمخزن البيانات الخاص بك
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false; 
}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); 

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

var app = builder.Build();

// --- 2. قسم خط التشغيل (الأمان والمرور) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// --- (تعديل جديد هنا) ---
// يجب أن نسأل الزائر: "من أنت؟" (Authentication) قبل أن نسأله "ماذا مسموح لك أن تفعل؟" (Authorization)
app.UseAuthentication(); 

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// أضف هذا السطر أيضاً ليدعم صفحات الدخول الجاهزة التي يوفرها النظام
app.MapRazorPages(); 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync(); 

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        // التعديل هنا: يجب طلب ApplicationUser وليس IdentityUser
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        
        // الآن نمررهم للميثود
        await DbInitializer.SeedData(roleManager, userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

app.Run();