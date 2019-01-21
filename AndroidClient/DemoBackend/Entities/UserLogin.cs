using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class UserLogin
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}
