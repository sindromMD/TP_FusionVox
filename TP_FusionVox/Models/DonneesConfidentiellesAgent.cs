using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TP_FusionVox.Models
{
    public class DonneesConfidentiellesAgent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} est obligatoire.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d{4}-\d{8}$", ErrorMessage = "Le format du {0} doit être YYYY-NNNNNNNNN.")]
        [Display(Name = "NumeroDeContrat")]
        public string NumeroDeContrat { get; set; }

        [Display(Name = "BankAccountInfo")]
        [DataType(DataType.CreditCard)]
        public string BankAccountInfo { get; set; }

        //[Required(ErrorMessage = "ValidationRequired")]
        //[Display(Name = "AgentId")]
        //public int AgentId { get; set; }

        [Display(Name = "Agent")]
        [ValidateNever]
        public virtual Agent Agent { get; set; }

    }
}
