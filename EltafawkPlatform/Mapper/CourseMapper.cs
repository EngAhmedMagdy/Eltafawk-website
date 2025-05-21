using EltafawkPlatform.Dto;
using EltafawkPlatform.Models;
using MongoDB.Bson;

namespace EltafawkPlatform.Mapper
{
    public static class CourseMapper
    {
        public static CourseUploadDto ToDto(this CourseModel model)
        {
            return new CourseUploadDto
            {
                Id = model.Id.ToString(),
                TeacherId = model.TeacherId.ToString(),
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                ImageFiles = model.Media.Images,
                YoutubeVideos = model.Media.YoutubeVideos,
                Texts = model.Media.Texts

            }; 
        }

        public static CourseModel ToModel(this CourseUploadDto dto)
        {
            return new CourseModel
            {
                TeacherId = ObjectId.Parse(dto.TeacherId),
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,
                Media = new CourseMediaModel
                {
                    Images = dto.ImageFiles,
                    YoutubeVideos = dto.YoutubeVideos,
                    Texts = dto.Texts
                }
            };
        }

        public static void UpdateModel(this CourseModel model, CourseUploadDto dto)
        {
            model.Title = dto.Title;
            model.Description = dto.Description;
            model.Category = dto.Category;
            model.Media.Images = dto.ImageFiles;
            model.Media.YoutubeVideos = dto.YoutubeVideos;
            model.Media.Texts = dto.Texts;
            model.Id = ObjectId.Parse(dto.TeacherId);
        }
    }

}
