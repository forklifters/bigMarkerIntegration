using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using bigMarkerIntegration.Models.ConferencesViewModels;
using Newtonsoft.Json.Linq;
using System.IO;

namespace bigMarkerIntegration.Controllers
{
    public class ConferencesController : Controller
    {

        public async Task<IActionResult> ViewAll()
        {
            var lines = System.IO.File.ReadLines(@"bigMarkerCreds.env");
            ViewData["Message"] = "Hello there, List of Conferences:";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://www.bigmarker.com/api/v1/conferences/");
            request.Headers["API-KEY"] = lines.ElementAt(0).ToString();
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //var responseString =  response.GetResponseStream().;

            //ViewData["Message"] = "Conference Created";
            ViewData["respString"] = responseString;
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
                        { "api_key", lines.ElementAt(0).ToString() },
                        { "api_secret", lines.ElementAt(1).ToString() },
                        { "host_id" , lines.ElementAt(2).ToString()},
                        { "type", model.Type},
                        { "topic", model.Topic }
                    };
                    //client.DefaultRequestHeaders.Add("","");
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://api.zoom.us/v1/Conference/create", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(responseString);
                    var startUrl = joResponse["start_url"].ToString();
                    ConferencesRepo.add(new Conference(joResponse));
                    ViewData["Message"] = "Conference Created";
                    ViewData["startUrl"] = startUrl;
                    //return View();
                    return RedirectToLocal(returnUrl);
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
