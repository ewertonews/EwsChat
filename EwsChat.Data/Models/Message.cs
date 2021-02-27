using System;

namespace EwsChat.Data.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int TargetRoomIn { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Texto { get; set; }
        public int ToUserId { get; set; }
    }
}
