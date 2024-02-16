namespace CromWood.Business.Constants
{
    public static class PermissionKeyConstant
    {
        public const string DashboardManagement = "dashboard_management";
        public const string FinanceManagement = "finance_management";
        public const string AssetManagement = "asset_management";
        public const string PropertyManagement = "property_management";
        public const string UserManagement = "user_management";
        public const string RoleManagement = "role_management";
        public const string TenancyManagement = "tenancy_management";
        public const string TenantManagement = "tenant_management";
        public const string LicenseManagement = "license_management";
        public const string NoticeAndClaimManagement = "notice_claim_management";
        public const string ComplaintManagement = "complaint_management";
        public const string MessageManagement = "message_management";
        public const string AssementManagement = "assesment_management";
        public const string ReportManagement = "report_management";
    }

    public static class PermissionConstant
    {
        public const string ViewAll = "View";
        public const string CanRead = "Read";
        public const string CanWrite = "Write";
        public const string CanDelete = "Delete";
    }

    public static class OtherConstants
    {
        public static Guid HousingBenfitTransactionMode = Guid.Parse("914d1a66-0ee7-45f6-ab77-19c928c5a426");
        public static Guid HousingBenefitStatementType = Guid.Parse("01289643-bba0-48d0-b34c-c69b466be7ec");
    }
}
