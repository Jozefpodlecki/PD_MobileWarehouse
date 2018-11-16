using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
