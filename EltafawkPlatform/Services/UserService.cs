using EltafawkPlatform.Dto;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EltafawkPlatform.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IMongoDatabase database) // Injected from MongoDB context
        {
            _userManager = userManager;
            //_usersCollection = database.GetCollection<ApplicationUser>("Users");
        }

        public async Task<List<ApplicationUser>> GetAllAsync() =>
            await _userManager.Users.ToListAsync();

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return null;

            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == objectId);
        }

        public async Task UpdateAsync(string id, ApplicationUser model)
        {
            if (!ObjectId.TryParse(id, out var objectId)) return;

            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == objectId);
            if (existingUser == null) return;

            // Update fields
            existingUser.FullName = model.FullName;
            existingUser.Type = model.Type;
            existingUser.Bio = model.Bio;
            existingUser.ProfileImage = model.ProfileImage;
            existingUser.Courses = model.Courses;
            existingUser.Rating = model.Rating;
            existingUser.IsApproved = model.IsApproved;

            await _userManager.UpdateAsync(existingUser);
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId)) return;

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == objectId);
            if (user == null) return;

            await _userManager.DeleteAsync(user);
        }

    }
}
