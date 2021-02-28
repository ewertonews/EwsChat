using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
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
                chatUsers.Add(user);
            });            
        }

        public async Task<IEnumerable<ChatUser>> GetAllUsersAsync()
        {
            return await Task.Run(() =>
            {
                return chatUsers;
            });
        }

        public Task<ChatUser> GetUserByIdAsync(string userId)
        {
            return Task.Run(() =>
            {
                return chatUsers.FirstOrDefault(u => u.UserId == userId);
            });
        }

        public async Task RemoveUserAsync(ChatUser user)
        {
            var possibleNonExistentUser = await GetUserByIdAsync(user.UserId);

            if (possibleNonExistentUser == null)
            {
                throw new NonExistentUserException("There is no user registered with given user id");
            }
            chatUsers.Remove(user);
        }
    }
}
