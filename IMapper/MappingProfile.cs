using AutoMapper;
using MVCCourse.Models;
using MVCCourse.ViewModels;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // --- 1. خريطة العرض (من الداتابيز إلى الجدول) ---
        CreateMap<ApplicationUserModel, UserListViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        // --- 2. خريطة الإنشاء (من الفورم إلى الداتابيز) ---
        // هذا هو السطر الذي ينقصك ويسبب الخطأ الحالي
        CreateMap<CreateUserViewModel, ApplicationUserModel>()
            // نتجاهل الـ PasswordHash لأن Identity سيقوم بتشفير الباسورد ووضعه يدوياً
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            
        // --- 3. خرائط أخرى مستقبلاً ---
        // CreateMap<Item, ItemDTO>();
    }
}