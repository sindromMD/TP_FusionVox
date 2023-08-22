using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class NewArtisteVM
    {


        public Artiste? Artiste { get; set; }

        [ValidateNever]
        public string? AncienneImage { get; set; } = "";
        [ValidateNever]
        public string? NomInitial { get; set; }
        public IEnumerable<SelectListItem>? GenresSelectList { get; set; }
        [ValidateNever]
        public int SelectedConcertId { get; set; }
        public List<int> SelectedConcertIds { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? ConsertsSelectList { get; set; }
        public IEnumerable<SelectListItem>? AgentSelectList { get; set; }
        //public List<ConcertCheckboxItem> ConcertCheckboxItems { get; set; } = new List<ConcertCheckboxItem>();
        //public class ConcertCheckboxItem
        //{
        //    public int ConcertId { get; set; }
        //    public string ConcertName { get; set; }
        //    public bool IsSelected { get; set; }
        //}

    }
}
