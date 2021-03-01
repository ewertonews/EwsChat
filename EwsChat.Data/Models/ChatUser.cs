using System;
using System.Collections.Generic;
using System.Text;

namespace EwsChat.Data.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public int ActiveRoomId { get; set; }

    }
}
