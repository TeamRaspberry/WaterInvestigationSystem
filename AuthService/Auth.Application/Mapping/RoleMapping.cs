using AutoMapper;
using Auth.Domain.Dto.RoleDto;
using Auth.Domain.Enity;
namespace Auth.Application.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
