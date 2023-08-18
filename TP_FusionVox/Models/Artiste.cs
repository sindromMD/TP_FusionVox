using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace TP_FusionVox.Models
{
    public class Artiste
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "ValidationMaxMin")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.Url)]
        public string? ImageURL { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "ValidationRequired")]
        [Display(Name = "Pays")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        public string Pays { get; set; }

        [Required(ErrorMessage = "ValidationRequired")]
        [Display(Name = "Debut")]
        [DataType(DataType.DateTime, ErrorMessage = "La date du début doit être valide")]
        public DateTime? DebutCarrier { get; set; }

        [Display(Name = "Biographie")]
        [StringLength(2500, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        [DataType(DataType.MultilineText)]
        public string? Biographie { get; set; }

        [Display(Name = "Vedette")]
        public bool EstVedette { get; set; }

        [Display(Name = "Abonnees")]
        public int NbAbonnees { get; set; }

        [Required(ErrorMessage = "ValidationRequired")]
        [Display(Name = "NbChansons")]
        [Range(0, int.MaxValue, ErrorMessage = "ValidationRange")]
        public int NbChansons { get; set; }

        [Required(ErrorMessage = "ValidationRequired")]
        [DataType(DataType.Text)]
        [Display(Name ="Agent")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        public string Agent { get; set; }

        [ForeignKey("GenreMusical")]
        [Display(Name = "GenreMusicalID")]
        [Required(ErrorMessage = "ValidationRequired")]
        public int? IdGenreMusical { get; set; }

        [Display(Name = "GenreMusical")]
        [ValidateNever]
        public virtual GenreMusical GenreMusical { get; set; }

        [Display(Name = "ListConcerts")]
        [ValidateNever]
        public virtual ICollection<Concert>? ListConcerts { get; set; }
    }
}
