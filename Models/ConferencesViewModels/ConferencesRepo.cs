using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bigMarkerIntegration.Models.ConferencesViewModels{

    public class ConferencesRepo{

        public static List<Conference> _conferencesRepo{ get; set; } = new List<Conference>();

        public static void add(Conference conference){
            if ( searchByID(conference) == -1){

                _conferencesRepo.Add(conference);
            }

        }
        public static List<Conference> getAll(){
            return _conferencesRepo;
        }

        public static int searchByID( Conference conf ){
            foreach(var conference in _conferencesRepo){
                if(conference.id.Equals(conf.id)){

                    return _conferencesRepo.IndexOf(conference);
                }
            }

            return -1;
        }

        public static void removeByID(string tid){
            var tempindex = -1;
            foreach (Conference Conference in _conferencesRepo){
                if(Conference.id.Equals(tid)){
                    tempindex = _conferencesRepo.IndexOf(Conference);
                    //_ConferencesRepo.Remove(Conference);
                }
            }

            if(tempindex != -1) _conferencesRepo.RemoveAt(tempindex);

        }
    }


}