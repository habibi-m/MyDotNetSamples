﻿namespace PermissionBasedAuthorization.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class Providers
        {
            public const string View = "Permissions.Providers.View";
            public const string Create = "Permissions.Providers.Create";
            public const string Edit = "Permissions.Providers.Edit";
            public const string Delete = "Permissions.Providers.Delete";
        }


    }
}
