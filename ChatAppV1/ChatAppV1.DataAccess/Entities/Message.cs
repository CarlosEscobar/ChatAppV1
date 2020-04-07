using System;

namespace ChatAppV1.DataAccess.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string SocketId { get; set; }
        public string Owner { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
