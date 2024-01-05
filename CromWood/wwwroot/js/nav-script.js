var navigations = [
    {
        url: '/dashboard', id: 'nav-dashboard'
    },
    {
        url: '/Property', id: 'nav-property-management'
    },
    {
        url: '/Asset', id: 'nav-asset-management'
    },
    {
        url: '/LicenseCertification', id: 'nav-license-certification'
    },
    {
        url: '/Tenancy', id: 'nav-tenancy-management'
    },
    {
        url: '/Tenant', id: 'nav-tenant-management'
    },
    {
        url: '/User', id: 'nav-settings'
    },
    {
        url: '/RolePermission', id: 'nav-settings'
    },
    {
        url: '/Auth', id: 'nav-login'
    },
]

let activeNavId = '';
if (window.location.pathname == '/') {
    activeNavId = 'nav-dashboard';

}
else {
    activeNavId = navigations.find(x => window.location.pathname.includes(x.url)).id;
}
if (activeNavId!='')
    document.getElementById(activeNavId).classList.add('active');