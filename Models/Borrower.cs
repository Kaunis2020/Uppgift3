using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift3.Models
{
    public class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Namnet ska anges")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Låntagarens namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Personnummer ska anges")]
        [DataType(DataType.Text)]
        [StringLength(20)]
        [Display(Name = "Personnummer")]
        public string SocialNumber { get; set; }

        [Required(ErrorMessage = "Adress ska anges")]
        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "E-post ska anges")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [Display(Name = "Låntagarens e-post")]
        public string Email { get; set; }

        [NotMapped]
        public virtual List<Loan> LoanedCDs { get; set; }

        [NotMapped]
        public string ListString => SocialNumber + ",  " + Name;
    }
}
