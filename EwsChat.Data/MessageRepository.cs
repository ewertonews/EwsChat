using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly HashSet<Message> _messages;
        private readonly IChatRoomRepository _chatRoomRepository;

        public MessageRepository(IChatRoomRepository chatRoomRepository)
        {
            _messages = new HashSet<Message>();
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await Task.Run(() =>
            {
                return _messages.OrderBy(m => m.CreatedAt);
            });
        }

        public async Task<IEnumerable<Message>> GetAllMessagesFromRoomAsync(int roomId)
        {
            return await Task.Run(() =>
            {
                return _messages.Where(m => m.TargetRoomId == roomId).OrderBy(m => m.CreatedAt);
            });
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesFromRoomAsync(int roomId, DateTime lastUpdate)
        {

            var allMessagesFromRoom = await GetAllMessagesFromRoomAsync(roomId);
            return allMessagesFromRoom.Where(r => r.CreatedAt >= lastUpdate);
        }

        public async Task AddMessageAsync(Message message)
        {
            await ValidateMessage(message);
            if (message.MessageId == null)
            {
                message.MessageId = Guid.NewGuid().ToString();
            }
            _messages.Add(message);
        }

        private async Task ValidateMessage(Message message)
        {
            if (message == null || message.TargetRoomId == 0)
            {
                throw new ArgumentNullException("The message is null or there is not target room id");
            }

            var targetRoom = await _chatRoomRepository.GetChatRoomByIdAsync(message.TargetRoomId);
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
