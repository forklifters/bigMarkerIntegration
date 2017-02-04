using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using System.Text;
using bigMarkerIntegration.Models.ConferencesViewModels;
using Newtonsoft.Json.Linq;
using System.IO;

namespace bigMarkerIntegration.Controllers
{
    public class ConferencesController : Controller
    {

        public static bool firstViewAll = true;
        public async Task<IActionResult> ViewAll()
        {
            //if( firstViewAll){
                var lines = System.IO.File.ReadLines(@"bigMarkerCreds.env");
                ViewData["Message"] = "Hello there, List of Conferences:";

                HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://www.bigmarker.com/api/v1/conferences/");
                request.Headers["API-KEY"] = lines.ElementAt(0).ToString();
                request.Method = "GET";
                //Gettin the response 
                var response = await request.GetResponseAsync();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //Converting the response to Json Onbject
                JObject joResponse = JObject.Parse(responseString);
                var ConferencesArray = (JArray) joResponse["conferences"];
                
                //Adding the conferences to the repository
                foreach (JObject conf in ConferencesArray){
                    ConferencesRepo.add(new Conference(conf));
                }
                ViewData["respString"] = responseString;

                firstViewAll = false;
           // }
            
            //return View();
            //return RedirectToLocal(returnUrl);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Hello there, create the Conference";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConferenceViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    var lines = System.IO.File.ReadLines(@"bigMarkerCreds.env");
                    var values = new Dictionary<string, string>
                    {
                        { "channel_id",model.ChannelID},
                        { "title", model.Title},
                        { "start_time" , model.StartTime},
                        { "purpose", model.Purpose},
                        { "duration", model.Duration }
                    };
                    client.DefaultRequestHeaders.Add("API_KEY",lines.ElementAt(0).ToString());
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://www.bigmarker.com/api/v1/conferences", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    //JObject joResponse = JObject.Parse(responseString);
                    //var startUrl = joResponse["start_url"].ToString();
                    //ConferencesRepo.add(new Conference(joResponse));
                    ViewData["Message"] = responseString;
                    //ViewData["startUrl"] = startUrl;
                    return View();
                    //return RedirectToLocal(returnUrl);
                }
            }

            return View(model);
            //return RedirectToLocal(returnUrl);
        }

        public IActionResult Update()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Delete(string ValueIneed)
        {

            using (var client = new HttpClient())
                {
                    var lines = System.IO.File.ReadLines(@"zoom2.env");

                    var values = new Dictionary<string, string>
                    {
                        { "api_key", lines.ElementAt(0).ToString() },
                        { "api_secret", lines.ElementAt(1).ToString() },
                        { "host_id" , lines.ElementAt(2).ToString()},
                        { "id" , ValueIneed}

                    };

                    var content = new FormUrlEncodedContent(values);

                    var response = await client.PostAsync("https://api.zoom.us/v1/Conference/delete", content);

                    //var responseString = await response.Content.ReadAsStringAsync();
                    //JObject joResponse = JObject.Parse(responseString);
                    //var startUrl = joResponse["start_url"].ToString();
                    //ConferencesRepo.add(new Conference(joResponse));

                }
            ViewData["Message"] = "Meetig with id "+ ValueIneed + " will be removed";
            ConferencesRepo.removeByID(ValueIneed);
            return RedirectToLocal(null);
            //return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Enter(string cid)
        {
            
            var lines = System.IO.File.ReadLines(@"bigMarkerCreds.env");
           
           /* HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://www.bigmarker.com/api/v1/conferences/enter");
            request.Headers["API-KEY"] = lines.ElementAt(0).ToString();
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var postData = "id="+cid;
                postData += "&attendee_name=Nabeel Abubaker&attendee_email=nabeel20055@gmail.com";
            var data = Encoding.ASCII.GetBytes(postData);

            using (var strm = await request.GetRequestStreamAsync()){
                strm.Write(data, 0, data.Length);
            }
            

            //Gettin the response 
            var response = await request.GetResponseAsync();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Converting the response to Json Onbject*/

            using (var client = new HttpClient())
                {
                    
                    var values = new Dictionary<string, string>
                    {
                        { "attendee_name", "Nabil" },
                        { "id", cid },
                        { "attendee_email" , "nabeel20055@gmail.com"},
                    };
                    client.DefaultRequestHeaders.Add("API-KEY",lines.ElementAt(0).ToString());
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://www.bigmarker.com/api/v1/conferences/enter", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(responseString);
                    ViewData["Message"] = responseString;
                    //ViewData["enterToken"] = joResponse["enter_token"].ToString();
                    //ViewData["enterUri"] = joResponse["enter_uri"].ToString(); 
                }            
            
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ConferencesController.ViewAll), "Conferences");
            }
        }
    }
}
