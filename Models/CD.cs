using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift3.Models
{
    public class CD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel ska anges")]
        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        // Foreign key 
        [Display(Name = "Artist")]
        [Required(ErrorMessage = "Artist måste anges")]
        public int ArtistID { get; set; }

        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; }

        [Required(ErrorMessage = "Kategori ska anges")]
        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Kategori")]

        public string Category { get; set; }

        [Required(ErrorMessage = "Utgivningsår ska anges")]
        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Utgivningsår")]
        public string Year { get; set; }
    }
}
