using EwsChat.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatRoomRepository
    {
        Task<HashSet<ChatRoom>> GetChatRoomsAsync();
        Task<ChatRoom> GetChatRoomByIdAsync(int id);
    }
}