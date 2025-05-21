
using EltafawkPlatform.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EltafawkPlatform.Services
{
    public class InboxService
    {
        private readonly IMongoCollection<InboxMessageModel> _messages;

        public InboxService(IMongoDatabase database)
        {
            _messages = database.GetCollection<InboxMessageModel>("InboxMessageModels");
        }

        public async Task<List<InboxMessageModel>> GetReceivedMessagesAsync(string userId) =>
            await _messages.Find(m => m.ReceiverId == userId).SortByDescending(m => m.SentAt).ToListAsync();

        public async Task<List<InboxMessageModel>> GetSentMessagesAsync(string userId) =>
            await _messages.Find(m => m.SenderId == userId).SortByDescending(m => m.SentAt).ToListAsync();

        public async Task<InboxMessageModel> SendMessageAsync(InboxMessageModel Model)
        {
            var message = new InboxMessageModel
            {
                SenderId = Model.SenderId,
                ReceiverId = Model.ReceiverId,
                Subject = Model.Subject,
                Body = Model.Body
            };
            await _messages.InsertOneAsync(message);
            return message;
        }

        public async Task AddReplyAsync(string messageId, ReplyModel reply)
        {
            var update = Builders<InboxMessageModel>.Update.Push("Replies", new ReplyModel
            {
                SenderId = reply.SenderId,
                Body = reply.Body
            });
            await _messages.UpdateOneAsync(m => m.Id == ObjectId.Parse(messageId), update);
        }

        public async Task MarkAsReadAsync(string messageId)
        {
            var update = Builders<InboxMessageModel>.Update.Set(m => m.IsRead, true);
            await _messages.UpdateOneAsync(m => m.Id == ObjectId.Parse(messageId), update);
        }
    }
}
