using Newtonsoft.Json;
using System.Collections.Generic;

namespace Notes.Services.DataModels
{
    public class NotesDataModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Favorite")]
        public string Favorite { get; set; }
    }

    public class NotesRootObject
    {
        [JsonProperty("data")]
        public List<NotesDataModel> NotesData { get; set; }
    }
}
