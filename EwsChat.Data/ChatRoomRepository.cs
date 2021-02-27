using EwsChat.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EwsChat.Data
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private HashSet<ChatRoom> ChatRooms;

        public ChatRoomRepository()
        {
            InitializeChatRooms();
        }

        public ChatRoom GetChatRoomById(int id)
        {
            return ChatRooms.FirstOrDefault(cr => cr.Id == id);
        }

        public HashSet<ChatRoom> GetChatRooms()
        {
            return ChatRooms;
        }

        private void InitializeChatRooms()
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
