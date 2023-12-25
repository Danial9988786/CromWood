using System.Security.Claims;

namespace CromWood.Helper
{

    public static class IdentityExtension
    {
        public static Guid GetId(ClaimsPrincipal identity)
        {
            return Guid.Parse(identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        public static Guid GetRoleId(ClaimsPrincipal identity)
        {
            return Guid.Parse(identity.Claims.First(x => x.Type == ClaimTypes.Role).Value);
        }
    }
}
