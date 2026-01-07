using Microsoft.AspNetCore.Identity;
using MVCCourse.Models;

public static class DbInitializer
{
    // التعديل هنا: غيرنا IdentityUser إلى ApplicationUser
    public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUserModel> userManager)
    {
        // 1. إضافة الرتب (تبقى كما هي لأن الرتب لم تتغير)
        string[] roleNames = { "SuperAdmin","Admin", "Employee", "Customer" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 2. إنشاء مستخدم Admin افتراضي
        var adminEmail = "sameer@smr.com";

        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            var newAdmin = new ApplicationUserModel // استخدام الكلاس الجديد حقك
            {
                UserName = "SMR",
                Email = adminEmail,
                FirstName = "smeer",      
                LastName = "ali",      
                PhoneNumber = "0550563839", 
                EmailConfirmed = true
            };
            
            // إنشاء المستخدم مع كلمة المرور
            var result = await userManager.CreateAsync(newAdmin, "Aa123456!");
            
            if (result.Succeeded)
            {
                // ربط المستخدم برتبة Admin
                await userManager.AddToRoleAsync(newAdmin, "SuperAdmin");
            }
        }
    }
}