using EwsChat.Data.Models;
using System;
using System.Collections.Generic;

namespace EwsChat.Data
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        IEnumerable<Message> GetAllMessages();
        IEnumerable<Message> GetAllMessagesFromRoom(int roomId);
        IEnumerable<Message> GetLatestMessagesFromRoom(int roomId, DateTime lastUpdate);
    }
}