using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EwsChat.Data
{
    public class MessageRepository : IMessageRepository
    {
        private HashSet<Message> _messages;
        private readonly IChatRoomRepository _chatRoomRepository;

        public MessageRepository(IChatRoomRepository chatRoomRepository)
        {
            _messages = new HashSet<Message>();
            _chatRoomRepository = chatRoomRepository;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _messages.OrderBy(m => m.CreatedAt);
        }

        public IEnumerable<Message> GetAllMessagesFromRoom(int roomId)
        {
            return _messages.Where(m => m.TargetRoomId == roomId).OrderBy(m => m.CreatedAt);
        }

        public IEnumerable<Message> GetLatestMessagesFromRoom(int roomId, DateTime lastUpdate)
        {
            return GetAllMessagesFromRoom(roomId).Where(r => r.CreatedAt >= lastUpdate);
            //var messagesFromRoom = GetAllMessagesFromRoom(roomId);
            //if (messagesFromRoom.Count() > 0)
            //{
            //    return GetAllMessagesFromRoom(roomId).Where(r => r.CreatedAt >= lastUpdate);
            //}
            //return messagesFromRoom;
        }

        public void AddMessage(Message message)
        {
            ValidateMessage(message);
            if (message.MessageId == null)
            {
                message.MessageId = Guid.NewGuid().ToString();
            }
            _messages.Add(message);
        }

        private void ValidateMessage(Message message)
        {
            if (message == null || message.TargetRoomId == 0)
            {
                throw new ArgumentNullException("The message is null or there is not target room id");
            }

            var targetRoom = _chatRoomRepository.GetChatRoomById(message.TargetRoomId);
            if (targetRoom == null)
            {
                throw new RoomNotFoundException("There is no chat room with the given TargetRoomId.");
            }

            if (string.IsNullOrEmpty(message.Texto))
            {
                throw new InvalidMessageException("Message text cannot be empty.");
            }
        }
    }
}
