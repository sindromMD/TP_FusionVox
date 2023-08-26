using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models;
using TP_FusionVox.ViewModels;

namespace TP_FusionVox.Services
{
    public interface IAgentService:IServiceBaseAsync<Agent>
    {
        IEnumerable<SelectListItem> ListAgentDisponible();
        Task<StatistiqueAgentVM> StatistiqueAgentAsync(Agent agent);
        Task<List<StatistiqueAgentVM>> StatistiqueTousAgentAsync();
    }
}
