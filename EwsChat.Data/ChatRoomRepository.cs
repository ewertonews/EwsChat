using EwsChat.Data.Models;
using System.Collections.Generic;

namespace EwsChat.Data
{
    public class ChatRoomRepository
    {
        private readonly HashSet<ChatRoom> ChatRooms;

        public ChatRoomRepository()
        {
            ChatRooms = new HashSet<ChatRoom>
            {
                new ChatRoom
                {
                    Id = 1001,
                    Name = "Punk Rock",
                    Messages = new HashSet<Message>(),
                    Participantes = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    Id = 1002,
                    Name = "Heavy Metal",
                    Messages = new HashSet<Message>(),
                    Participantes = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    Id = 1003,
                    Name = "Grunge",
                    Messages = new HashSet<Message>(),
                    Participantes = new HashSet<ChatUser>()
                }
            };
        }

    }
}
