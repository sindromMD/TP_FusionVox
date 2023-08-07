using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TP2.Models;

namespace TP_FusionVox.ViewModels
{
    public class GenreMusicalVM
    {
        public GenreMusical? GenreMusical { get; set; }
        [ValidateNever]
        public string? AncienneImage { get; set; } = "";
    }
}
