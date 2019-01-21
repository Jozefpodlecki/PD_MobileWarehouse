using Common;
using SQLite;

namespace Data_Access_Layer
{
    public class City : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string Name { get; set; }
    }
}
