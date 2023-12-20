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


            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetFirstName(src.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetLastName(src.Name)));

            CreateMap<Asset, AssetViewModel>()
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => $"{(src.AssetType != null ? src.AssetType.Name + " " : "")}{src.HouseNoStreet} ,{src.Borough}"));
            CreateMap<AssetModel, Asset>().ReverseMap();

            CreateMap<Property, PropertyViewModel>();
            CreateMap<PropertyModel, Property>().ReverseMap();
            CreateMap<PropertyInsuranceModel, PropertyInsurance>().ReverseMap();
            CreateMap<PropertyKeyModel, PropertyKey>().ReverseMap();
        }

        private string GetFirstName(string name)
        {
            var nameArray = name.Split(' ');

            return nameArray.Length == 2 ? name.Split(' ')[0] : name;
        }
        private string GetLastName(string name)
        {
            var nameArray = name.Split(' ');

            return nameArray.Length == 2 ? name.Split(' ')[1] : " ";
        }
    }
}
