using System;

namespace EwsChat.Data.Models
{
    public class Message
    {
        public string MessageId { get; set; }
        public int TargetRoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Texto { get; set; }
        public int ToUserId { get; set; }
    }
}
