using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP_FusionVox.Models
{
    public class Concert
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "ValidationRequired")]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "ValidationMaxMin")]
        public string Nom { get; set; }

        [Display(Name = "Description")]
        [StringLength(2500, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "ImageURL")]
        [DataType(DataType.Url)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "ValidationRequired")]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime, ErrorMessage = "La date du début doit être valide")] //I18N required
        public DateTime DateConcert { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [Display(Name = "Pays")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        public string Pays { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [Display(Name = "Ville")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        public string Ville { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [Display(Name = "Scene")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        public string Scene { get; set; }

        [Required(ErrorMessage = "ValidationRequired")]
        [Display(Name = "NbTotalBillets")]
        [Range(0, int.MaxValue, ErrorMessage = "ValidationRange")]
        public int NbTotalBillets { get; set; }

        [Display(Name = "NbBilletsVendu")]
        [Range(0, int.MaxValue, ErrorMessage = "ValidationRange")]
        public int? NbBilletsVendu { get; set; }

        [Display(Name = "PrixBillet")]
        [Required(ErrorMessage = "ValidationRequired")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ValidationRange")]
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public decimal PrixBillet { get; set; }

        [Required]
        [Display(Name = "ListArtistes")]
        [ValidateNever]
        public virtual ICollection<Artiste> ListArtistes { get; set; }
    }
}
