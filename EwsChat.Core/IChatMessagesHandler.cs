using EwsChat.Data.Models;
using System;
using System.Collections.Generic;

namespace EwsChat.Core
{
    public interface IChatMessagesHandler
    {
        void AddMessageToRoom(int roomId, Message message);
        HashSet<Message> GetAllMessagesFromRoom(int roomId);
        HashSet<Message> GetAllMessagesFromLastRequest(int roomId, DateTime lastRequest);

    }
}
