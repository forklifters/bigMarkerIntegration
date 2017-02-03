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
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

    }
}
