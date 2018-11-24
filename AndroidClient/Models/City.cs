using Newtonsoft.Json;

namespace Client.Models
{
    public class City : Java.Lang.Object
    {

        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}