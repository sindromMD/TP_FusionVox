using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace TP2.Models
{
    public class Artiste
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Le nom de l'artiste est obligatoire")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Le nom de l'artiste doit comporter entre {2} et {1} caractères.")]
        [Display(Name = "Nom de l'artiste")]
        public string Nom { get; set; }

        [Display(Name = "Image URL")]
        [DataType(DataType.Url)]
        public string? ImageURL { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Pays d'origine est obligatoire")]
        [Display(Name = "Pays d'origine")]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "Le nom du pays doit comporter entre {2} et {1} caractères.")]
        public string Pays { get; set; }

        [Required(ErrorMessage = "La date du début est obligatoire")]
        [Display(Name = "Date de début de la carrière")]
        [DataType(DataType.DateTime, ErrorMessage = "La date du début doit être valide")]
        public DateTime DebutCarrier { get; set; }

        [Display(Name = "Biographie de l'artiste")]
        [StringLength(2500, MinimumLength = 0, ErrorMessage = "La biographie doit comporter entre {2} et {1} caractères.")]
        [DataType(DataType.MultilineText)]
        public string? Biographie { get; set; }

        [Display(Name = "Artiste vedette")]
        public bool EstVedette { get; set; }

        [Display(Name = "Nombre d'abonnees")]
        public int NbAbonnees { get; set; }

        [Required(ErrorMessage = "Le nombre de chansons est obligatoire")]
        [Display(Name = "Nombre de chansons")]
        [Range(0, int.MaxValue, ErrorMessage = "Le nombre de chansons doit être supérieur ou égal à {1}.")]
        public int NbChansons { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire, choisissez l'agent")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 0, ErrorMessage = "Le nom de l'agent doit comporter entre {2} et {1} caractères.")]
        public string Agent { get; set; }

        [ForeignKey("GenreMusical")]
        [Required(ErrorMessage = "Le genre musical est obligatoire")]
        public int? IdGenreMusical { get; set; }

        [ValidateNever]
        public virtual GenreMusical GenreMusical { get; set; }

    }
}
