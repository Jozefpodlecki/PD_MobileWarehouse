using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class UserToken
    {
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        public string LoginProvider { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
