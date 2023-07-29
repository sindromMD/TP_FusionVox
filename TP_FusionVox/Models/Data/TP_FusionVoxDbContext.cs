using Microsoft.EntityFrameworkCore;

namespace TP_FusionVox.Models.Data
{
    public class TP_FusionVoxDbContext :DbContext
    {
        public TP_FusionVoxDbContext(DbContextOptions<TP_FusionVoxDbContext> options) : base(options)
        {

        }
    }
}
