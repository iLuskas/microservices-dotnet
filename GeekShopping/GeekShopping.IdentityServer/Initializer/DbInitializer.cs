using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySqlContext _mySqlContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(MySqlContext mySqlContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mySqlContext = mySqlContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "lucaum-admin",
                Email = "lucaum-admin@soft.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (21) 97045-3061",
                FirstName = "Lucas",
                LastName = "Admin"
            };

            _userManager.CreateAsync(adminUser, "Lucas123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser,
                IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(adminUser, new Claim[] { 
                new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, $"{adminUser.FirstName} {adminUser.LastName}")
            } ).Result;            
            
            ApplicationUser clientUser = new ApplicationUser()
            {
                UserName = "lucaum-client",
                Email = "lucaum-client@soft.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (21) 97045-3061",
                FirstName = "Lucas",
                LastName = "Client"
            };

            _userManager.CreateAsync(clientUser, "Lucas123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(clientUser, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(clientUser, new Claim[] { 
                new Claim(JwtClaimTypes.Name, $"{clientUser.FirstName} {clientUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, clientUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, clientUser.LastName),
                new Claim(JwtClaimTypes.Role, $"{clientUser.FirstName} {clientUser.LastName}")
            } ).Result;
        }
    }
}
