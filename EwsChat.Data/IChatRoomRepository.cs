using EwsChat.Data.Models;
using System.Collections.Generic;

namespace EwsChat.Data
{
    public interface IChatRoomRepository
    {
        HashSet<ChatRoom> GetChatRooms();
        ChatRoom GetChatRoomById(int id);
    }
}