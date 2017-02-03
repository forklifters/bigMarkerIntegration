using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace bigMarkerIntegration.Models.ConferencesViewModels{

    public class Conference{

        public Conference( JObject jobj ){
            
            this.Duration = jobj["duration"].ToString();
            this.Title = jobj["title"].ToString();
            this.Purpose = jobj["purpose"].ToString();
            this.StartTime = jobj["start_time"].ToString();
            this.ConferenceAddress = jobj["conference_address"].ToString();
            this.ChannelID = jobj["channel_id"].ToString();
            this.id = jobj["id"].ToString();
            this.Presenters = new List<Presenter>();

            var PresentersArray = (JArray) jobj["presenters"];
            foreach (JObject pres in PresentersArray){
                this.Presenters.Add(new Presenter(pres));
            }     
        }

        public string ConferenceAddress { get; set; }
        public string Title { get; set; }
        public string id { get; set; }
        public string Purpose { get; set; }
        public List<Presenter> Presenters { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string ChannelID { get; set; }
    }
}