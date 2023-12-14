using AutoMapper;
using CromWood.Business.Models;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;

namespace CromWood.Mapper
{
    public class UserProfiler : Profile
    {
        public UserProfiler()
        {
            CreateMap<Permission, PermissionModel>().ReverseMap();
            CreateMap<RolePermissionModel, RolePermission>().ReverseMap();
            CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.NoOfUsers, opt => opt.MapFrom(src => src.Users.Count));

            CreateMap<RoleModel, Role>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermission)).ReverseMap();
        }
    }
}
