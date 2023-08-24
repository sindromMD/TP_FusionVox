using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TP_FusionVox.Models
{
    public class Agent
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
        [Display(Name = "DateNaissance")]
        [DataType(DataType.DateTime, ErrorMessage = "La date de naissance doit être valide")]
        public DateTime? DateNaissance { get; set; }

        [Display(Name = "Biographie")]
        [StringLength(2500, MinimumLength = 0, ErrorMessage = "ValidationMaxMin")]
        [DataType(DataType.MultilineText)]
        public string? Biographie { get; set; }

        [Display(Name = "SalaireMensuel")]
        [Required(ErrorMessage = "ValidationRequired")]
        [Range(0.01, float.MaxValue, ErrorMessage = "ValidationRange")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", NullDisplayText = "N/A")]
        public float SalaireMensuel { get; set; }

        [Display(Name = "Curriel")]
        [EmailAddress]
        public string Curriel { get; set; }

        [Display(Name = "ListArtistes")]
        [ValidateNever]
        public virtual ICollection<Artiste>? ListArtistes { get; set; }

        [ForeignKey("DonneesConfidentiellesAgent")]
        [Display(Name = "infoConfidentiellesAgent")]
        public int DonneesConfidentiellesAgentID { get; set; }

        [Display(Name = "infoConfidentiellesAgent")]
        [ValidateNever]
        public virtual DonneesConfidentiellesAgent? DonneesConfidentiellesAgent { get; set; }
    }
}
