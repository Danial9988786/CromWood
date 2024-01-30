using AutoMapper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
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
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => $"{(src.AssetType != null ? $"{src.AssetType.Name} " : "")}{(string.IsNullOrEmpty(src.Borough)? src.HouseNoStreet:string.Join(", ",src.HouseNoStreet,src.Borough))}"))
                .ForMember(dest => dest.TotalUnit, opt => opt.MapFrom(src => src.Properties.Count));
            CreateMap<AssetModel, Asset>().ReverseMap();

            CreateMap<Property, PropertyViewModel>();
            CreateMap<PropertyModel, Property>().ReverseMap();
            CreateMap<PropertyInsuranceModel, PropertyInsurance>().ReverseMap();
            CreateMap<PropertyKeyModel, PropertyKey>().ReverseMap();

            CreateMap<LicenseCertificate, LicenseCertificateViewModel>().ReverseMap();
            CreateMap<LicenseCertificate, LicenseCertificateModel>().ReverseMap();

            CreateMap<Tenancy, TenancyViewModel>().ReverseMap();
            CreateMap<TenancyTenant, TenancyTenantModel>().ReverseMap();
            CreateMap<Tenancy, TenancyModel>().ReverseMap();
            CreateMap<Tenant, TenantModel>().ReverseMap();

            CreateMap<TenantViewModel, Tenant>().ReverseMap()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.AddressLine1}, {src.County} ,{src.City}, {src.PostCode}"));

            CreateMap<TenancyNote, TenancyNoteModel>().ReverseMap();
            CreateMap<TenancyDocument, TenancyDocumentModel>().ReverseMap();

            CreateMap<TenancyMessage, TenancyMessageViewModel>().ReverseMap();
            CreateMap<TenancyMessage, TenancyMessageModel>().ReverseMap();

            CreateMap<UnitUtility, UnitUtilityModel>().ReverseMap();
            CreateMap<UnitUtility, UnitUtilityViewModel>().ReverseMap();
            CreateMap<UnitUtilityReading, UnitUtilityReadingModel>().ReverseMap();
            CreateMap<UnitUtilityReading, UnitUtilityReadingViewModel>().ReverseMap();
            CreateMap<UnitUtilityDocument, UnitUtilityDocumentModel>().ReverseMap();
            CreateMap<UnitUtilityDocument, UnitUtilityDocumentViewModel>().ReverseMap();

            CreateMap<TenancyStatement, StatementModel>().ReverseMap();
            CreateMap<TenancyStatement, StatementViewModel>().ReverseMap();
            CreateMap<StatementTransaction, StatementTransactionModel>().ReverseMap();
            CreateMap<StatementTransaction, StatementTransactionViewModel>().ReverseMap();
            CreateMap<StatementDocument, StatementDocumentModel>().ReverseMap();
            CreateMap<StatementDocument, StatementDocumentViewModel>().ReverseMap();

            CreateMap<RecurringCharge, RecurringChargeModel>().ReverseMap();
            CreateMap<RecurringCharge, RecurringChargeViewModel>().ReverseMap();

            CreateMap<Notice, NoticeModel>().ReverseMap();
            CreateMap<Notice, NoticeViewModel>().ReverseMap();

            CreateMap<Claim, ClaimModel>().ReverseMap();
            CreateMap<Claim, ClaimViewModel>().ReverseMap();

            CreateMap<Complaint, ComplaintModel>().ReverseMap();
            CreateMap<Complaint, ComplaintViewModel>().ReverseMap();

            CreateMap<ComplaintComment, ComplaintCommentViewModel>().ReverseMap();
            CreateMap<ComplaintComment, ComplaintCommentModel>().ReverseMap();

            CreateMap<Message, MessageViewModel>().ReverseMap();
            CreateMap<Message, MessageModel>().ReverseMap();

            CreateMap<MessageRecipient, MessageRecipientModel>().ReverseMap();
            CreateMap<MessageRecipient, MessageRecipientViewModel>().ReverseMap();

            CreateMap<PropertyAssesment, PropertyAssesmentModel>().ReverseMap();
            CreateMap<PropertyAssesment, PropertyAssesmentViewModel>().ReverseMap();
            
            CreateMap<PropertyInspectionItem, PropertyInspectionItemModel>().ReverseMap();
            CreateMap<PropertyInspectionItem, PropertyInspectionItemViewModel>().ReverseMap();
            CreateMap<PropertyInspectionItemImage, PropertyInspectionItemImageViewModel>().ReverseMap();
            CreateMap<PropertyInspectionItemImage, PropertyInspectionItemImageModel>().ReverseMap();
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
