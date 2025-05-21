namespace EltafawkPlatform.Dto
{
    public class InboxMessageDto
    {
        public string Id { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }

        public List<ReplyDto> Replies { get; set; } = new();
    }
}
