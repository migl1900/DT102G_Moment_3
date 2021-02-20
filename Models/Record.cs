using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_Moment3.Models
{
    public class Record
    {
        // Properties and dependencies
        public int RecordId { get; set; }

        [Required(ErrorMessage = "Ange ditt för och efternamn")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ange vilket år skivan släpptes")]
        [Display(Name = "Årtal")]
        public DateTime Year { get; set; }

        [Required(ErrorMessage = "Ange artist")]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
