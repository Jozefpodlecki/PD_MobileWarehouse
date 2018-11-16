using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string NIP { get; set; }
    }
}
