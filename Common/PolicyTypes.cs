using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common
{
    public static class PolicyTypes
    {
        public static List<string> Properties = typeof(PolicyTypes)
            .GetNestedTypes()
            .SelectMany(ty => ty.GetFields())
            .Select(ty => ty.GetValue(null))
            .Cast<string>()
            .Union(typeof(PolicyTypes)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && fi.FieldType == typeof(string))
                .Select(fi => fi.GetValue(null))
                .Cast<string>())
            .ToList();

        public const string ScanBarcode = nameof(ScanBarcode);

        public const string ScanQRCode = nameof(ScanQRCode);

        public static class Users
        {
            [Description("Can see users")]
            public const string Read = nameof(Users) + "." + nameof(Read);
            [Description("Can add user")]
            public const string Add = nameof(Users) + "." + nameof(Add);
            [Description("Can update user")]
            public const string Update = nameof(Users) + "." + nameof(Update);
            [Description("Can remove user")]
            public const string Remove = nameof(Users) + "." + nameof(Remove);
        }

        public static class Roles
        {
            [Description("Can see roles")]
            public const string Read = nameof(Roles) + "." + nameof(Read);
            [Description("Can add role")]
            public const string Add = nameof(Roles) + "." + nameof(Add);
            [Description("Can update role")]
            public const string Update = nameof(Roles) + "." + nameof(Update);
            [Description("Can remove role")]
            public const string Remove = nameof(Roles) + "." + nameof(Remove);
        }

        public static class Invoices
        {
            [Description("Can see invoices")]
            public const string Read = nameof(Invoices) + "." + nameof(Read);
            [Description("Can add invoice")]
            public const string Add = nameof(Invoices) + "." + nameof(Add);
            [Description("Can update invoice")]
            public const string Update = nameof(Invoices) + "." + nameof(Update);
            [Description("Can remove invoice")]
            public const string Remove = nameof(Invoices) + "." + nameof(Remove);
        }

        public static class Counterparties
        {
            [Description("Can see counterparties")]
            public const string Read = nameof(Counterparties) + "." + nameof(Read);
            [Description("Can add counterparty")]
            public const string Add = nameof(Counterparties) + "." + nameof(Add);
            [Description("Can update counterparty")]
            public const string Update = nameof(Counterparties) + "." + nameof(Update);
            [Description("Can remove counterparty")]
            public const string Remove = nameof(Counterparties) + "." + nameof(Remove);
        }

        public static class Notes
        {
            [Description("Can see notes")]
            public const string Read = nameof(Notes) + "." + nameof(Read);
            [Description("Can add note")]
            public const string Add = nameof(Notes) + "." + nameof(Add);
            [Description("Can update note")]
            public const string Update = nameof(Notes) + "." + nameof(Update);
            [Description("Can remove note")]
            public const string Remove = nameof(Notes) + "." + nameof(Remove);
        }

        public static class Products
        {
            [Description("Can see products")]
            public const string Read = nameof(Products) + "." + nameof(Read);
            [Description("Can add product")]
            public const string Add = nameof(Products) + "." + nameof(Add);
            [Description("Can update product")]
            public const string Update = nameof(Products) + "." + nameof(Update);
            [Description("Can remove product")]
            public const string Remove = nameof(Products) + "." + nameof(Remove);
        }

        public static class Attributes
        {
            [Description("Can see attribute list")]
            public const string Read = nameof(Attributes) + "." + nameof(Read);
            [Description("Can add attribute")]
            public const string Add = nameof(Attributes) + "." + nameof(Add);
            [Description("Can update attribute")]
            public const string Update = nameof(Attributes) + "." + nameof(Update);
            [Description("Can remove attribute")]
            public const string Remove = nameof(Attributes) + "." + nameof(Remove);
        }

        public static class Locations
        {
            [Description("Can see attribute list")]
            public const string Read = nameof(Locations) + "." + nameof(Read);
            [Description("Can add attribute")]
            public const string Add = nameof(Locations) + "." + nameof(Add);
            [Description("Can update attribute")]
            public const string Update = nameof(Locations) + "." + nameof(Update);
            [Description("Can remove attribute")]
            public const string Remove = nameof(Locations) + "." + nameof(Remove);
        }
    }
}
