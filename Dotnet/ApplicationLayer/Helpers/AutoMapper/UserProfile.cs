using AutoMapper;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.DomainLayer.Entities;
using System.Globalization;

namespace DotNet6Mediator.ApplicationLayer.Helpers.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //USER REQUEST => USER
            CreateMap<UserDtoRequest, User>() // UserDtoRequest has Password field!
                .ForMember(Obj => Obj.BirthDate, opt => opt.MapFrom(DtoObj => DateTime.ParseExact(DtoObj.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .ForMember(Obj => Obj.UserRole, opt => opt.MapFrom(DtoObj => new Role() { RoleName = DtoObj.RoleName }))
                .ReverseMap();
            //USER => USER RESPONSE
            CreateMap<User, UserDtoResponse>() // UserDtoResponse does not has Password field!
                .ForMember(DtoObj => DtoObj.BirthDate, opt => opt.MapFrom(Obj => Obj.BirthDate.ToString("dd-MM-yyyy")))
                .ForMember(DtoObj => DtoObj.RoleName,  opt => opt.MapFrom(Obj => (Obj.UserRole == null) ? null : Obj.UserRole.RoleName))
                .ReverseMap();
        }
    }
}
