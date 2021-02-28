using EwsChat.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EwsChat.Data
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private HashSet<ChatRoom> _chatRooms;

        public ChatRoomRepository()
        {
            InitializeChatRooms();
        }

        public ChatRoom GetChatRoomById(int id)
        {
            return _chatRooms.FirstOrDefault(cr => cr.Id == id);
        }

        public HashSet<ChatRoom> GetChatRooms()
        {
            return _chatRooms;
        }

        private void InitializeChatRooms()
        {
            _chatRooms = new HashSet<ChatRoom>
            {
                new ChatRoom
                {
                    Id = 1001,
                    Name = "Punk Rock",
                    Participantes = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    Id = 1002,
                    Name = "Heavy Metal",
                    Participantes = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    Id = 1003,
                    Name = "Grunge",
                    Participantes = new HashSet<ChatUser>()
                }
            };
        }
    }
}
