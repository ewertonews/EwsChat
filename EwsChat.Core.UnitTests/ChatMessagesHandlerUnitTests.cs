using EwsChat.Data;
using EwsChat.Data.Models;
using NUnit.Framework;
using System;

namespace EwsChat.Core.UnitTests
{
    public class ChatMessagesHandlerUnitTests
    {
        private IChatMessagesHandler _chatMessagesHandler;
        private IChatRoomRepository _chatRoomRepository;

        [SetUp]
        public void Setup()
        {
            //here it would be a mock if were connecting to a real DB.
            _chatRoomRepository = new ChatRoomRepository();
            _chatMessagesHandler = new ChatMessagesHandler(_chatRoomRepository);           
        }

        //[Test]
        //public void AddMessageToRoomShouldAddMessageToRoomOfGivenId()
        //{
        //    int roomId = 1002;
        //    var targetRoom = _chatRoomRepository.GetChatRoomById(roomId);
        //    var message = new Message()
        //    {
        //        MessageId = Guid.NewGuid().ToString(),
        //        CreatedAt = DateTime.UtcNow,
        //        TargetRoomId = roomId,
        //        Texto = "'sup guys!"
        //    };

        //    _chatMessagesHandler.AddMessageToRoom(roomId, message);           

        //    Assert.That(targetRoom.Messages, Is.Not.Empty);
        //    Assert.That(targetRoom.Messages.Contains(message));
        //}        

        //[Test]
        //public void GetAllMessagesFromLastRequestShouldReturnMessagesWithCreatedAtGreaterOrEqualLastRequest()
        //{
        //    int roomId = 1003;
        //    AddMessagesToRoom(roomId);

        //    var lastRequest = DateTime.UtcNow.AddMinutes(1);

        //    var missedMessages = _chatMessagesHandler.GetAllMessagesFromLastRequest(roomId, lastRequest);

        //    Assert.That(missedMessages.Count, Is.EqualTo(2));
        //}

        //private void AddMessagesToRoom(int roomId)
        //{
        //    var message1 = new Message()
        //    {
        //        CreatedAt = DateTime.UtcNow,
        //        TargetRoomId = roomId,
        //        Texto = "'sup guys!"
        //    };
        //    var message2 = new Message()
        //    {
        //        CreatedAt = DateTime.UtcNow.AddMinutes(1),
        //        TargetRoomId = roomId,
        //        Texto = "Heeyy!"
        //    };
        //    var message3 = new Message()
        //    {
        //        CreatedAt = DateTime.UtcNow.AddMinutes(2),
        //        TargetRoomId = roomId,
        //        Texto = "What are you guys up to?"
        //    };

        //    _chatMessagesHandler.AddMessageToRoom(roomId, message1);
        //    _chatMessagesHandler.AddMessageToRoom(roomId, message2);
        //    _chatMessagesHandler.AddMessageToRoom(roomId, message3);
        //}

    }
}