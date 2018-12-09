using Client;
using Common;
using System.Collections.Generic;

namespace Client
{
    public static class Constants
    {
        public const string ConfigurationFilePath = "appsettings.json";
        public const string CurrentUser = nameof(CurrentUser);
        public const string CredentialResource = nameof(CredentialResource);
        public const string Credential = nameof(Credential);
        public const string ConfigResource = nameof(ConfigResource);
        public const string Language = nameof(Language);
        public const string JWTResource = nameof(JWTResource);
        public const string JWTResourceJWT = nameof(JWTResourceJWT);
        public const string JWTResourceToken = nameof(JWTResourceToken);

        public static KeyValuePair<int, string>[] MenuItemClaimMap =
        {
            new KeyValuePair<int, string>( Resource.Id.ScanBarcodeActionBarMenuItem, PolicyTypes.ScanBarcode ),
            new KeyValuePair<int, string>( Resource.Id.ScanOCRActionBarMenuItem, PolicyTypes.ScanQRCode ),
            new KeyValuePair<int, string>( Resource.Id.ProductsMenuItem, PolicyTypes.Products.Read ),
            new KeyValuePair<int, string>( Resource.Id.UsersMenuItem, PolicyTypes.Users.Read ),
            new KeyValuePair<int, string>( Resource.Id.RolesMenuItem, PolicyTypes.Roles.Read ),
            new KeyValuePair<int, string>( Resource.Id.CounterpartiesMenuItem, PolicyTypes.Counterparties.Read ),
            new KeyValuePair<int, string>( Resource.Id.InvoicesMenuItem, PolicyTypes.Invoices.Read ),
            new KeyValuePair<int, string>( Resource.Id.GoodsReceivedNoteMenuItem, PolicyTypes.Notes.Read ),
            new KeyValuePair<int, string>( Resource.Id.GoodsDispatchedNoteMenuItem, PolicyTypes.Notes.Read )
        };

        public static Dictionary<string, int> ErrorsMap = new Dictionary<string, int>
        {
            { nameof(Resource.String.USER_NOT_FOUND), Resource.String.USER_NOT_FOUND }
        };

        public static Dictionary<string, int> LanguageResourceMap = new Dictionary<string, int>()
        {
            { "pol", Resource.Id.PolishRadioButton},
            { "eng", Resource.Id.EnglishRadioButton}
        };
    }
}