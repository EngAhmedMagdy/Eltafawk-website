using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EltafawkPlatform.Models
{
    public class OfferModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId CourseId { get; set; }
        public string Title { get; set; }
        public double Discount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Category { get; set; }
    }

}
