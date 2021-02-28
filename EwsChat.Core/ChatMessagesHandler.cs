using EwsChat.Data;
using EwsChat.Data.Models;
using System;
using System.Collections.Generic;

namespace EwsChat.Core
{
    public class ChatMessagesHandler : IChatMessagesHandler
    {
        private readonly IChatRoomRepository ChatRoomRepository;

        public ChatMessagesHandler(IChatRoomRepository chatRoomRepository)
        {
            this.ChatRoomRepository = chatRoomRepository;
        }

        public void AddMessageToRoom(int roomId, Message message)
        {
            //ChatRoom targetRoom = ChatRoomRepository.GetChatRoomById(roomId);
            //if (targetRoom == null)
            //{
            //    throw new RoomNotFoundException("There is no chat room with the given ID.");
            //}
            //if (string.IsNullOrEmpty(message.Texto))
            //{
            //    throw new InvalidMessageException("Message text cannot be empty.");
            //}
            //targetRoom.Messages.Add(message);          
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
