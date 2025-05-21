using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ApplicationUser : MongoUser
{
    // Add custom properties here
    public string Type { get; set; } = ""; //student , teacher , admin
    public string FullName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string ProfileImage { get; set; } = "";
    public List<ObjectId> Courses { get; set; } = new();
    public double Rating { get; set; }
    public bool IsApproved { get; set; }
}
