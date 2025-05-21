using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EltafawkPlatform.Dto
{
    public class CourseUploadDto
    {
        public string Id { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? ImageFiles { get; set; }
        public string? YoutubeVideos { get; set; } 
        public string? Texts { get; set; }
    }

}
