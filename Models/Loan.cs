using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift3.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        // Foreign key 
        [Display(Name = "Låntagare")]
        public int BorrowerID { get; set; }

        [ForeignKey("BorrowerID")]
        [Display(Name = "Låntagare")]
        public virtual Borrower Borrower { get; set; }

        // Foreign key 
        [Display(Name = "CD")]
        public int CD_ID { get; set; }

        [ForeignKey("CD_ID")]
        public virtual CD CD { get; set; }

        [Required(ErrorMessage = "Lånedatum ska anges")]
        [ReadOnly(true)]
        [Display(Name = "Lånedatum")]
        public string LoanDate { get; set; } = DateTime.Now.ToShortDateString();

        [Required(ErrorMessage = "Förfallodatum ska anges")]
        [ReadOnly(true)]
        [Display(Name = "Förfallodatum")]
        public string BackDate { set; get; } = DateTime.Now.AddDays(30).ToShortDateString();

        [NotMapped]
        public List<SelectListItem> CDList { get; set; }
    }
}
