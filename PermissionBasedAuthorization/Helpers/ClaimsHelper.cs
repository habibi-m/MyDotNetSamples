using Microsoft.AspNetCore.Identity;
using PermissionBasedAuthorization.Models;
using System.Reflection;
using System.Security.Claims;
using PermissionBasedAuthorization.Constants;

namespace PermissionBasedAuthorization.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            }
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }

        public static void GetAllPermissions(this List<RoleClaimsViewModel> allPermissions)
        {
            var permissionClass = typeof(Permissions);
            var permissions = permissionClass.GetTypeInfo().DeclaredNestedTypes;

            foreach (var policy in permissions)
            {
                FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo fi in fields)
                {
                    allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
                }
            }
        }
    }
}
