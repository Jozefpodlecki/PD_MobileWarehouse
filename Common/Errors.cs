using System;

namespace Common
{
    public static class Errors
    {
        public const string USER_NOT_FOUND = nameof(USER_NOT_FOUND);
        public const string USER_EXISTS = nameof(USER_EXISTS);
        public const string USERS_ASSIGNED_TO_ROLE = nameof(USERS_ASSIGNED_TO_ROLE);

        public const string ROLE_NOT_FOUND = nameof(ROLE_NOT_FOUND);
        public const string ROLE_EXISTS = nameof(ROLE_EXISTS);

        public const string ATTRIBUTE_NOT_FOUND = nameof(ATTRIBUTE_NOT_FOUND);
        public const string ATTRIBUTE_EXISTS = nameof(ATTRIBUTE_EXISTS);

        public const string PRODUCT_NOT_FOUND = nameof(PRODUCT_NOT_FOUND);
        public const string PRODUCT_EXISTS = nameof(PRODUCT_EXISTS);

        public const string INVALID_PASSWORD = nameof(INVALID_PASSWORD);
    }
}
