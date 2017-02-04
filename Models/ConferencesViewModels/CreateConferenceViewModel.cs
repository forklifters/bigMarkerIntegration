using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bigMarkerIntegration.Models.ConferencesViewModels
{
    public class CreateConferenceViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Channel ID")]
        public string ChannelID { get; set; }

        [Required]
        [DataTypeAttribute(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        
        [Display(Name = "Purpose")]
        [DataType(DataType.MultilineText)]
        public string Purpose { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Duration")]
        public string Duration {get; set;}

    }
}
