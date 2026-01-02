using AutoMapper;
using MVCCourse.Models;
using MVCCourse.ViewModels;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // --- 1. خرائط المستخدمين ---
        CreateMap<ApplicationUser, UserViewModel>()
            .ForMember(dest => dest.FullName, 
                       opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        // --- 2. خرائط المنتجات (مثال لما تضيفها مستقبلاً) ---
        // CreateMap<Item, ItemDTO>();
        
        // --- 3. خرائط أخرى ---
        // CreateMap<Order, OrderViewModel>();
    }
}