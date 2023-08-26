using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.ViewModels;

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

        public async Task<StatistiqueAgentVM> StatistiqueAgentAsync(Agent agent)
        {
            var statsAgents = await _dbContext.Agent
             .Where(ag => ag.Id == agent.Id)
             .Select(st => new StatistiqueAgentVM
             {
                 Id = st.Id,
                 Nom = st.Nom,
                 ImageURL = st.ImageURL,
                 Curriel = st.Curriel,
                 DateNaissance = st.DateNaissance,
                 SalaireMensuel = st.SalaireMensuel,
                 ListArtistes = st.ListArtistes,
                 NbArtistes = st.ListArtistes.Count(),
                 DonneesConfidentiellesAgent = st.DonneesConfidentiellesAgent,
             }).FirstOrDefaultAsync();

            return statsAgents;
        }

        public async Task<List<StatistiqueAgentVM>> StatistiqueTousAgentAsync()
        {
            var agents = await _dbContext.Agent.ToListAsync();
            var listStatsAgents = new List<StatistiqueAgentVM>();

            foreach (var agent in agents) 
            {
                listStatsAgents.Add(await StatistiqueAgentAsync(agent));
            }
            return listStatsAgents;
        }
    }
}
