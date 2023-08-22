using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox.Services
{
    public class AgentService : ServiceBaseAsync<Agent>, IAgentService
    {
        public AgentService(TP_FusionVoxDbContext dbContext) : base(dbContext) { }
        public IEnumerable<SelectListItem> ListAgentDisponible()
        {
            var AgentDisponibleList = _dbContext.Agent/*.Where(gm => gm.EstDisponible == true)*/
             .Select(x => new SelectListItem
             {
                 Text = x.Nom,
                 Value = x.Id.ToString()
             }).OrderBy(t => t.Text);

            return AgentDisponibleList;
        }
    }
}
