using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EltafawkPlatform.Models
{
    public class CourseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId TeacherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public CourseMediaModel Media { get; set; } = new();
        public DateTime CreatedAt { get; set; }

        public List<ObjectId> Offers { get; set; } = new();
    }

    public class CourseMediaModel
    {
        public string Images { get; set; } 
        public string YoutubeVideos { get; set; }
        public string Texts { get; set; } 
    }

}
