using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models;

namespace TP_FusionVox.ViewModels
{
    public class ConcertVM
    {
        public Concert? Concert { get; set; }

        public IEnumerable<SelectListItem> ArtisteSelectList;
    }
}
