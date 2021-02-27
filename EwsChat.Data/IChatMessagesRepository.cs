using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatMessagesRepository
    {
        bool AddMessageToRoom(int roomId, string message);
        HashSet<Message> GetAllMessagesFromRoom(int roomId);
        HashSet<Message> GetAllMessagesFromLastRequest(int roomId, DateTime lastRequest);

    }
}
