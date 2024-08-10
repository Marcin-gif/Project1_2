using AutoMapper;
using Project1_2.Entities;
using Project1_2.Models;

namespace Project1_2
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile() 
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address() 
                { 
                    City= dto.City,
                    Street= dto.Street,
                    PostalCode= dto.PostalCode
                }));

            CreateMap<PutRestaurantDto, Restaurant>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateDishDto, Dish>();
            CreateMap<PutDishDto, Dish>();
                
        }
    }
}
