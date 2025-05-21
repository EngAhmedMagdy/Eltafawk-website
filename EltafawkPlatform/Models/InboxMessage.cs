using MongoDB.Bson;

namespace EltafawkPlatform.Models
{
    public class InboxMessageModel
    {
        public ObjectId Id { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        public List<ReplyModel> Replies { get; set; } = new(); // Optional threaded replies
    }

   

}
