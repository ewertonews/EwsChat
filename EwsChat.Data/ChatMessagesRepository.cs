using EwsChat.Data.Models;
using System;
using System.Collections.Generic;

namespace EwsChat.Data
{
    public class ChatMessagesRepository : IChatMessagesRepository
    {
        public bool AddMessageToRoom(int roomId, string message)
        {
            throw new NotImplementedException();
        }

        public HashSet<Message> GetAllMessagesFromLastRequest(int roomId, DateTime lastRequest)
        {
            throw new NotImplementedException();
        }

        public HashSet<Message> GetAllMessagesFromRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
