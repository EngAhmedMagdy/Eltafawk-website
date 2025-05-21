using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EltafawkPlatform.Models
{
    public class TeacherModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }

        public List<ObjectId> Courses { get; set; } = new();
        public double Rating { get; set; }

        public bool IsApproved { get; set; }
    }


}
