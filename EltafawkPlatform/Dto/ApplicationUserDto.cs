using MongoDB.Bson;

namespace EltafawkPlatform.Dto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Type { get; set; } = ""; //student , teacher , admin
        public string FullName { get; set; } = "";
        public string Bio { get; set; } = "";
        public string ProfileImage { get; set; } = "";
        public List<ObjectId> Courses { get; set; } = new();
        public double Rating { get; set; }
        public bool IsApproved { get; set; }
    }

}
