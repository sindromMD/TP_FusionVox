using Microsoft.AspNetCore.Mvc.Rendering;
using TP_FusionVox.Models;

namespace TP_FusionVox.Services
{
    public interface IAgentService:IServiceBaseAsync<Agent>
    {
        IEnumerable<SelectListItem> ListAgentDisponible();
    }
}
