using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class ChatUserRespository : IChatUserRespository
    {
        private readonly HashSet<ChatUser> chatUsers;

        public ChatUserRespository()
        {
            chatUsers = new HashSet<ChatUser>();
        }


        public async Task AddUserAsync(ChatUser user)
        {
            await Task.Run(() => {
                var possibleExistentUser = chatUsers.FirstOrDefault(u => u.NickName == user.NickName);
                if (possibleExistentUser != null)
                {
                    throw new UserAlreadyExistsException("There is already a user registered with the given NickName");
                }
                if (user.UserId == null)
                {
                    user.UserId = Guid.NewGuid().ToString();
                }
                chatUsers.Add(user);
            });            
        }


        public async Task<IEnumerable<ChatUser>> GetUsersOfRoomAsync(int roomId)
        {
            //TODO:throw RoomNotFoundException when invalid room;
            return await Task.Run(() => {
                return chatUsers.Where(u => u.ActiveRoomId == roomId);
            });
        }


        public async Task<IEnumerable<ChatUser>> GetAllUsersAsync()
        {
            return await Task.Run(() =>
            {
                return chatUsers;
            });
        }



        public async Task<ChatUser> GetUserByIdAsync(string userId)
        {
            var searchedUser = await Task.Run(() =>
            {
                return chatUsers.FirstOrDefault(u => u.UserId == userId);
            });

            if(searchedUser == null)
            {
                throw new UserNotFoundException("There is no user registered with given user id");
            }

            return searchedUser;
        }

        public async Task<ChatUser> UpdateUserAsync(ChatUser updatedUser)
        {
            var userToRemove = await GetUserByIdAsync(updatedUser.UserId);
            chatUsers.Remove(userToRemove);
            chatUsers.Add(updatedUser);
            return updatedUser;
        }

        public async Task RemoveUserAsync(string userId)
        {
            var userToRemove = await GetUserByIdAsync(userId);

            if (userToRemove == null)
            {
                throw new UserNotFoundException("There is no user registered with given user id.");
            }
            chatUsers.Remove(userToRemove);
        }
        
    }
}
