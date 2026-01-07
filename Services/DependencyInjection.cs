using System.Reflection;

namespace MVCCourse.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // سحب كل الكلاسات التي تنتهي بـ Service من Assembly المشروع
        var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

        foreach (var serviceType in serviceTypes)
        {
            // البحث عن الـ Interface اللي يطبقه هذا الكلاس (مثلاً IUserService)
            var interfaceType = serviceType.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + serviceType.Name);

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, serviceType);
            }
        }

        return services;
    }
}