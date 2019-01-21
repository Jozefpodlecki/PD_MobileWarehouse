using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class UserRole
    {
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        [OneToOne]
        public User User { get; set; }

        [ForeignKey(typeof(Role))]
        public int RoleId { get; set; }

        [OneToOne]
        public Role Role { get; set; }
    }
}
