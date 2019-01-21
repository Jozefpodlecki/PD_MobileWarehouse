using System.Collections.Generic;

namespace Common.DTO
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
