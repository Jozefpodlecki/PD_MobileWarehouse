using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class UserClaim
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
