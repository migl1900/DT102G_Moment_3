using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_Moment3.Models
{
    public class Artist
    {
        // Properties
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Ange artistnamn")]
        [Display(Name = "Artist")]
        public string Name { get; set; }
    }
}
