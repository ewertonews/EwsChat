using System.Collections.Generic;

namespace EwsChat.Data.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<ChatUser> Participantes { get; set; }
    }
}
