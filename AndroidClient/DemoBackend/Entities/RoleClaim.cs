using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class RoleClaim
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [OneToOne]
        public Role Role { get; set; }

        [ForeignKey(typeof(Role))]
        public int RoleId { get; set; }
        
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
