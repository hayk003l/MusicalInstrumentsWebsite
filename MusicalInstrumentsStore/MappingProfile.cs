using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Item, ItemForCreationDto>().ReverseMap();
            CreateMap<Item, ItemForUpdatingDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Item, ItemForOrderDto>().ReverseMap();

            CreateMap<Description, DescriptionForCreationDto>().ReverseMap();
            CreateMap<Description, DescriptionDto>().ReverseMap();
            CreateMap<Description, DescriptionForUpdatingDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<OrderForAdminDto, Order>().ReverseMap();
            CreateMap<OrderForUserDto, Order>().ReverseMap();
            CreateMap<OrderForCreationDto, Order>().ReverseMap();

            CreateMap<ShippingDetails, ShippingDetailsDto>().ReverseMap();
            CreateMap<ShippingDetailsDto, ShippingDetailsForCreationDto>().ReverseMap();
            CreateMap<ShippingDetails, ShippingDetailsForCreationDto>().ReverseMap();   
        }
    }
}
