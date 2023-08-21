using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using static TP_FusionVox.ViewModels.NewArtisteVM;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public class ConcertService : ServiceBaseAsync<Concert>, IConcertService
    { 
        public ConcertService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }

        public IEnumerable<SelectListItem> ListConcerts()
        {
            var concertsList = _dbContext.Concert.Select(x => new SelectListItem
            {
                Text = x.Nom,
                Value = x.Id.ToString()
            }).OrderBy(c => c.Text);
            return concertsList;
        }

    }
}
