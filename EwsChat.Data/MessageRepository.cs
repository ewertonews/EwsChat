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
            if(message.CreatedAtString != null)
            {
                message.CreatedAt = DateTime.Parse(message.CreatedAtString);
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

            if (string.IsNullOrEmpty(message.Text))
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
            string welcomeText = "Let's have some nice talks, guys!";

            Message welcome1 = new Message()
            {
                CreatedAt = DateTime.Now,
                Text = welcomeText,
                TargetRoomId = 1001,
                ToUserName = "Everyone"
            };

            Message welcome2 = new Message()
            {
                CreatedAt = DateTime.Now,
                Text = welcomeText,
                TargetRoomId = 1002,
                ToUserName = "Everyone"
            };

            Message welcome3 = new Message()
            {
                CreatedAt = DateTime.Now,
                Text = welcomeText,
                TargetRoomId = 1003,
                ToUserName = "Everyone"
            };
            _ = AddMessageAsync(welcome1);
            _ = AddMessageAsync(welcome2);
            await AddMessageAsync(welcome3);
        }
    }
}
