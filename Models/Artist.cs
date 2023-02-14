using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift3.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Namnet ska anges")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Artistens namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kategori ska anges")]
        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Kategori")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Verksamma år ska anges")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [Display(Name = "Verksamma år")]
        public string ActiveYears { get; set; }

        [Required(ErrorMessage = "Land ska anges")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [Display(Name = "Land")]
        public string Country { get; set; }

        [NotMapped]
        public virtual List<CD> Cds { get; set; }
    }
}
