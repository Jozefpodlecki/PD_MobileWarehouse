using Newtonsoft.Json;

namespace Client.Models
{
    public class Counterparty : Java.Lang.Object
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string PostalCode { get; set; }

        [JsonProperty]
        public string Street { get; set; }

        [JsonProperty]
        public City City { get; set; }

        [JsonProperty]
        public string PhoneNumber { get; set; }

        [JsonProperty]
        public string NIP { get; set; }

        public override string ToString() => Name;
    }
}