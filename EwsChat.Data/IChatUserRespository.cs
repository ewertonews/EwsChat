using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatUserRespository
    {
        Task AddUserAsync(ChatUser user);

        Task RemoveUserAsync(ChatUser user);

        Task<IEnumerable<ChatUser>> GetAllUsersAsync();

        Task<ChatUser> GetUserByIdAsync(string userId);        
    }
}
