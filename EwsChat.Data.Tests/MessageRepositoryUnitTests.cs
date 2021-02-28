using EwsChat.Data.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class MessageRepositoryUnitTests
    {
        private IMessageRepository _messageRepository;
        private IChatRoomRepository _chatRoomRepository;

        [SetUp]
        public void Setup()
        {
            //would be a mock if using actual DB
            _chatRoomRepository = new ChatRoomRepository();
            _messageRepository = new MessageRepository(_chatRoomRepository);
        }

        [Test]
        public void AddMessageShouldAddMessageSuccessfully()
        {
            var message = new Message
            {
                TargetRoomId = 1001,
                MessageId = Guid.NewGuid().ToString(),
                Texto = "Hello",
                CreatedAt = DateTime.UtcNow
            };

            _messageRepository.AddMessage(message);

            var messagesFromRoom = _messageRepository.GetAllMessagesFromRoom(1001);

            Assert.That(messagesFromRoom.Contains(message));
        }

        [Test]
        public void AddMessageWithoutIdShouldCreateIdAndAddMessageSuccessfully()
        {
            var message = new Message
            {
                TargetRoomId = 1001,
                Texto = "Hello",
                CreatedAt = DateTime.UtcNow
            };

            _messageRepository.AddMessage(message);

            var messageFromRoom = _messageRepository.GetAllMessagesFromRoom(1001).FirstOrDefault() ;

            Assert.That(messageFromRoom, Is.Not.Null);
            Assert.That(messageFromRoom.MessageId, Is.Not.Null);
        }

        [Test]
        public void AddMessageShouldThrowNullArgumentExceptoin()
        {
            Message message = null;
            Assert.That(() => _messageRepository.AddMessage(message), Throws.ArgumentNullException);
        }


        [Test]
        public void AddMessageShouldThrowInvalidMessageExceptoin()
        {
            int roomId = 1002;
            string emptyMessage = string.Empty;
            var message = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = roomId,
                Texto = emptyMessage
            };

            Assert.That(() => _messageRepository.AddMessage(message), Throws.TypeOf<InvalidMessageException>());
        }
        [Test]
        public void AddMessageShouldThrowRoomNotFoundException()
        {
            int idOfNonExistentRoom = 1004;
            var message = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = idOfNonExistentRoom,
                Texto = "'sup guys!"
            };

            Assert.That(() => _messageRepository.AddMessage(message), Throws.TypeOf<RoomNotFoundException>());
        }

        [Test]
        public void GetAllMessagesShouldReturnAllMessagesInRepository()
        {
            AddMessagesToRoom(1001);

            var allMessages = _messageRepository.GetAllMessages();

            Assert.That(allMessages.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetAllMessagesFromRoomShouldReturnOnlyMessagesWithGivenRoomId()
        {
            var punkRockRoomId = 1001;
            AddMessagesToRoom(punkRockRoomId);

            var heavyMetalRoomId = 1002;
            AddMessagesToRoom(heavyMetalRoomId);

            var messagesFromHeavyMetalRoom = _messageRepository.GetAllMessagesFromRoom(heavyMetalRoomId);

            Assert.That(messagesFromHeavyMetalRoom.Any(m => m.TargetRoomId != heavyMetalRoomId), Is.False);
        }

        [Test]
        public void GetAllMessagesFromRoomShouldReturnEmptyIEnumerable()
        {
            var punkRockRoomId = 1001;

            var messagesFromHeavyMetalRoom = _messageRepository.GetAllMessagesFromRoom(punkRockRoomId);

            Assert.That(messagesFromHeavyMetalRoom, Is.Empty);
        }

        [Test]
        public void GetAllMessagesFromRoomShouldEmptyForNonExistentRoom()
        {
            var unknownRoomId = 8888;

            var messages = _messageRepository.GetAllMessagesFromRoom(unknownRoomId);

            Assert.That(messages, Is.Empty);
        }

        [Test]
        public void GetLatestMessagesFromRoomShouldReturMessagesFromGivenDatetime()
        {
            var lastUpdate = DateTime.UtcNow.AddMinutes(1);
            var roomId = 1002;

            AddMessagesToRoom(roomId);

            var messagesFromDate = _messageRepository.GetLatestMessagesFromRoom(roomId, lastUpdate);

            Assert.That(messagesFromDate.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetLatestMessagesFromRoomShouldReturnEmptyIEnumerable()
        {
            var lastUpdate = DateTime.UtcNow.AddMinutes(3);
            var roomId = 1002;

            AddMessagesToRoom(roomId);

            var messagesFromDate = _messageRepository.GetLatestMessagesFromRoom(roomId, lastUpdate);

            Assert.That(messagesFromDate, Is.Empty);
        }

        private void AddMessagesToRoom(int roomId)
        {
            var message1 = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = roomId,
                Texto = "'sup guys!"
            };
            var message2 = new Message()
            {
                CreatedAt = DateTime.UtcNow.AddMinutes(1),
                TargetRoomId = roomId,
                Texto = "Heeyy!"
            };
            var message3 = new Message()
            {
                CreatedAt = DateTime.UtcNow.AddMinutes(2),
                TargetRoomId = roomId,
                Texto = "What are you guys up to?"
            };

            _messageRepository.AddMessage(message1);
            _messageRepository.AddMessage(message2);
            _messageRepository.AddMessage(message3);
        }
    }
}
