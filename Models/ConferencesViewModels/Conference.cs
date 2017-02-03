using Newtonsoft.Json.Linq;

namespace bigMarkerIntegration.Models.ConferencesViewModels{

    public class Conference{

        public Conference( JObject jobj ){
            
            this.Duration = jobj["duration"].ToString();
            this.Topic = jobj["topic"].ToString();
            this.Type = jobj["type"].ToString();
            this.StartTime = jobj["start_time"].ToString();
            this.startUrl = jobj["start_url"].ToString();
            this.joinUrl = jobj["join_url"].ToString();
            this.id = jobj["id"].ToString();
            
        }

        public string startUrl { get; set; }
        public string joinUrl { get; set; }
        public string id { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
    }
}