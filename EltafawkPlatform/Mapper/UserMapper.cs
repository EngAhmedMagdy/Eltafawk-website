using EltafawkPlatform.Dto;
using EltafawkPlatform.Models;

namespace EltafawkPlatform.Mapper
{
    public static class UserMapper
    {
        public static ApplicationUserDto ToDto(this ApplicationUser user)
        {

            return new ApplicationUserDto
            {
                Id = user.Id.ToString(),
                Type = user.Type,
                FullName = user.FullName,
                Bio = user.Bio,
                ProfileImage = user.ProfileImage,
                Courses = user.Courses,
                Rating = user.Rating,
                IsApproved = user.IsApproved
            };
        }

        public static ApplicationUser ToModel(this ApplicationUserDto dto)
        {
            return new ApplicationUser
            {
                Id = new MongoDB.Bson.ObjectId(dto.Id),
                Type = dto.Type,
                FullName = dto.FullName,
                Bio = dto.Bio,
                ProfileImage = dto.ProfileImage,
                Courses = dto.Courses,
                Rating = dto.Rating,
                IsApproved = dto.IsApproved
            };
        }
        public static void UpdateModel(this ApplicationUser user, ApplicationUserDto updateModel)
        {
            user.Type = updateModel.Type;
            user.FullName = updateModel.FullName;
            user.Bio = updateModel.Bio;
            user.ProfileImage = updateModel.ProfileImage;
            user.Courses = updateModel.Courses;
            user.Rating = updateModel.Rating;
            user.IsApproved = updateModel.IsApproved;
        }
    }

}
