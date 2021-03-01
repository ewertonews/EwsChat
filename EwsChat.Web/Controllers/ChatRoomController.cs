using EwsChat.Data;
using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwsChat.Web.Controllers
{
    [Route("api/ewschat/rooms")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var chatRooms = await _chatRoomRepository.GetChatRoomsAsync();
            return new OkObjectResult(chatRooms);
        }

        [HttpGet("{chatRoomId}")]
        public async Task<IActionResult> Get(int chatRoomId)
        {
            try
            {
                var chatRoom = await _chatRoomRepository.GetChatRoomByIdAsync(chatRoomId);
                return new OkObjectResult(chatRoom);
            }
            catch (RoomNotFoundException rnf)
            {
                return new NotFoundObjectResult(rnf.Message);
            }
        }

        //TODO: Post room
        //TODO: Delete room
    }
}
