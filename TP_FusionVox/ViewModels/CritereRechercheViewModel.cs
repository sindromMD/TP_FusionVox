using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace TP_FusionVox.ViewModels
{
    public class CritereRechercheViewModel
    {
        [Display(Name = "Artiste vedette")]
        public string ChoiPourPersonnageVedette { get; set; }

        [Display(Name = "POP")]
        public bool EstGenreMusicalPOP { get; set; }

        [Display(Name = "HIP-HOP")]
        public bool EstGenreMusicalHipHop { get; set; }

        [Display(Name = "ÉLECTRO")]
        public bool EstGenreMuscialElectro { get; set; }

        [Display(Name = "Nombre d'abonnees")]
        [Range(0, int.MaxValue, ErrorMessage = "Le nombre doit être supérieur ou égal à {1}")]
        public int? NbMaxAbonnes { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Le nombre doit être supérieur ou égal à {1}")]
        [Display(Name = "Nombre d'abonnees")]
        public int? NbMinAbonnes { get; set; }

        [Display(Name = "Nom de l'artiste")]
        public string InputNomArtiste { get; set; }

        public CritereRechercheViewModel() { }
    }
}
