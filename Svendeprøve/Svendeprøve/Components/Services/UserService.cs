namespace Svendeprøve.Components.Services
{
    using Microsoft.AspNetCore.Identity;
    using Svendeprøve.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Service til håndtering af brugeroperationer (CRUD) med ASP.NET Core Identity.
    /// </summary>
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initialiserer en ny instans af UserService med en UserManager.
        /// </summary>
        /// <param name="userManager">UserManager til håndtering af ApplicationUser.</param>
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Henter en liste over alle brugere.
        /// </summary>
        /// <returns>En liste af ApplicationUser.</returns>
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        /// <summary>
        /// Opretter en ny bruger med angivet email og adgangskode.
        /// </summary>
        /// <param name="email">Brugerens email.</param>
        /// <param name="password">Brugerens adgangskode.</param>
        /// <returns>Resultatet af oprettelsesoperationen.</returns>
        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Opdaterer en eksisterende brugers email.
        /// </summary>
        /// <param name="userId">ID for brugeren, der skal opdateres.</param>
        /// <param name="newEmail">Ny email for brugeren.</param>
        /// <returns>Resultatet af opdateringsoperationen.</returns>
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

        /// <summary>
        /// Sletter en bruger baseret på deres ID.
        /// </summary>
        /// <param name="userId">ID for brugeren, der skal slettes.</param>
        /// <returns>Resultatet af sletteoperationen.</returns>
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
