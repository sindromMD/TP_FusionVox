using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox.Models
{
    public class GenreMusical
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "ValidationMaxMin")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        //[Required(ErrorMessage = "L'image est obligatoire")]
        [Display(Name = "ImageURL")]
        [DataType(DataType.Url)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "EstDisponible")]
        public bool EstDisponible { get; set; } = true;

        [Display(Name ="List")]
        [ValidateNever]
        public virtual List<Artiste>? Artistes { get; set; }
    }
}
