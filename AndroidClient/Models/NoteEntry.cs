using Newtonsoft.Json;

namespace Client.Models
{
    public class NoteEntry : Java.Lang.Object
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public Location Location { get; set; }
    }
}