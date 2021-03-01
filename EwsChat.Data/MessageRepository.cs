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
            InitializeMessages().Wait();
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
            await ValidadeRoom(roomId);
            return _messages.Where(m => m.TargetRoomId == roomId).OrderBy(m => m.CreatedAt);          
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesFromRoomAsync(int roomId, DateTime lastUpdate)
        {
            await ValidadeRoom(roomId);
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

            await ValidadeRoom(message.TargetRoomId);

            if (string.IsNullOrEmpty(message.Texto))
            {
                throw new InvalidMessageException("Message text cannot be empty.");
            }
        }

        private async Task ValidadeRoom(int roomId)
        {
            var targetRoom = await _chatRoomRepository.GetChatRoomByIdAsync(roomId);
            if (targetRoom == null)
            {
                throw new RoomNotFoundException("There is no chat room with the given TargetRoomId.");
            }
        }

        private async Task InitializeMessages()
        {
            string welcomeText = "Welcome to EwsChat! We hope uou enjoy it and make some friends!";

            Message welcome1 = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                Texto = welcomeText,
                TargetRoomId = 1001
            };

            Message welcome2 = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                Texto = welcomeText,
                TargetRoomId = 1002
            };

            Message welcome3 = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                Texto = welcomeText,
                TargetRoomId = 1003
            };
            _ = AddMessageAsync(welcome1);
            _ = AddMessageAsync(welcome2);
            await AddMessageAsync(welcome3);
        }
    }
}
