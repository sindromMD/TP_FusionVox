using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TP_FusionVox.Models.Data;

namespace TP2.Models
{
    public class GenreMusical
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Le nom du genre musical est obligatoire")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Le nom du genre musical doit comporter entre {2} et {1} caractères.")]
        [Display(Name = "Genre de musique")]
        public string Nom { get; set; }

        [Required (ErrorMessage = "L'image est obligatoire")]
        [Display(Name = "Image URL")]
        [DataType(DataType.Url)]
        public string ImageUrl { get; set; }

        [Display(Name = "Description du genre musical")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "La description doit comporter entre {2} et {1} caractères.")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [ValidateNever]
        public virtual List<Artiste>? Artistes { get; set; }
    }
}
