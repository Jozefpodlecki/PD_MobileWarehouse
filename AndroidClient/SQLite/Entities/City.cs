using Common;

namespace Data_Access_Layer
{
    public class City : BaseEntity, IName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
