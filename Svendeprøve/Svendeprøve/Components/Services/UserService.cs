namespace Svendeprøve.Components.Services
{
    using Microsoft.AspNetCore.Identity;
    using Svendeprøve.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    // Serviceklasse til håndtering af brugeroperationer i vores ASP.NET Core-applikation.
    // Bruger Identity framework til at administrere brugere (oprettelse, opdatering, finding og sletning).
    // Funktioner: 
    // UserService -> Initialiserer UserManager via dependency injection.
    // Task<List<ApplicationUser>> -> Henter en liste af alle brugere fra Identity-databasen.
    // Task<IdentityResult> -> Opretter en ny bruger med angivet email og adgangskode og Opdaterer en eksisterende brugers email og brugernavn baseret på bruger-ID.
    // og Sletter en bruger baseret på bruger-ID længere nede


    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }
        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> UpdateUserAsync(string userId, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Bruger ikke fundet" });
            }

            user.UserName = newEmail;
            user.Email = newEmail;
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Bruger ikke fundet" });
            }

            return await _userManager.DeleteAsync(user);
        }
    }
}
