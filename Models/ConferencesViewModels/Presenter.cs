using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace bigMarkerIntegration.Models.ConferencesViewModels{

    public class Presenter{

        public Presenter( JObject jobj ){
            
            this.conference_id = jobj["conference_id"].ToString();
            this.first_name = jobj["first_name"].ToString();
            this.last_name = jobj["last_name"].ToString();
            this.email = jobj["email"].ToString();
            this.member_id = jobj["member_id"].ToString();

        }

        public string member_id{get; set;}
        public string conference_id{get; set;}
        public string first_name{get; set;}
        public string last_name{get; set;}
        public string email{get; set;}
    }
}


