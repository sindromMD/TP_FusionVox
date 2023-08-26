using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.Utility;

namespace TP_FusionVox.DbInitializer
{
    public class DbInitializer : IdbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TP_FusionVoxDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TP_FusionVoxDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            // Exécuter les migrations sont effectuées
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            // Créer les rôles suivants si aucun rôle ne figure dans la bd
            if (!_roleManager.RoleExistsAsync(AppConstants.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(AppConstants.AdminRole))
                    .GetAwaiter().GetResult();

                _roleManager.CreateAsync(new IdentityRole(AppConstants.AgentRole))
                    .GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(AppConstants.ArtisteRole))
                    .GetAwaiter().GetResult();

                // Créer un User pour le rôle Admin
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Alexei@FusionVox.com",
                    Email = "Alexei@FusionVox.com",
                    Pseudonymes = "ALEXEIculaxiz",
                    PhoneNumber = "1234567890"
                }, "Admin123*").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Alexei@FusionVox.com");
                _userManager.AddToRoleAsync(user, AppConstants.AdminRole)
                    .GetAwaiter().GetResult();
                // Créer deux Users pour le rôle Agent
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Simion@FusionVox.com",
                    Email = "Simion@FusionVox.com",
                    Pseudonymes = "SAgent",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                }, "Agent123*").GetAwaiter().GetResult();
                ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Simion@FusionVox.com");
                _userManager.AddToRoleAsync(user2, AppConstants.AgentRole)
                    .GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Iurie@FusionVox.com",
                    Email = "Iurie@FusionVox.com",
                    Pseudonymes = "IAgent",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                }, "Agent123*").GetAwaiter().GetResult();
                ApplicationUser user3 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Iurie@FusionVox.com");
                _userManager.AddToRoleAsync(user3, AppConstants.AgentRole)
                    .GetAwaiter().GetResult();

                // Créer deux Users pour le rôle Artiste
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Eminem@FusionVox.com",
                    Email = "Eminem@FusionVox.com",
                    Pseudonymes = "Eminem",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                }, "Artiste123*").GetAwaiter().GetResult();
                ApplicationUser user4 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Eminem@FusionVox.com");
                _userManager.AddToRoleAsync(user4, AppConstants.ArtisteRole)
                    .GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Adele@FusionVox.com",
                    Email = "Adele@FusionVox.com",
                    Pseudonymes = "Adele",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                }, "Artiste123*").GetAwaiter().GetResult();
                ApplicationUser user5 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Adele@FusionVox.com");
                _userManager.AddToRoleAsync(user5, AppConstants.ArtisteRole)
                    .GetAwaiter().GetResult();
            }
        }
    }

}
