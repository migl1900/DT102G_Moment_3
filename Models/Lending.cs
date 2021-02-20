using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_Moment3.Models
{
    public class Lending
    {
        // Properties and dependencies
        public int LendingId { get; set; }

        [Required(ErrorMessage = "Ange namn på den som lånar skivan")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ange datum för utlåning")]
        [Display(Name = "Utlåningsdatum")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Ange vilken skiva som lånats ut")]
        [Display(Name = "Skiva")]
        public int RecordId { get; set; }
        public Record Record { get; set; }
    }
}
