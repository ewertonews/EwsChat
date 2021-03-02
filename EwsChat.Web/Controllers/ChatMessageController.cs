using EwsChat.Data;
using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwsChat.Web.Controllers
{
    [Route("api/ewschat/messages")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public ChatMessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // GET: api/<ChatMessagesController>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(int roomId)
        {
            try
            {
                var messages = await _messageRepository.GetAllMessagesFromRoomAsync(roomId);
                return new OkObjectResult(messages);
            }
            catch (RoomNotFoundException rnf)
            {
                return new BadRequestObjectResult(rnf.Message);
            }
        }

        [HttpGet("{roomId}/{lastUpdated}")]
        public async Task<IActionResult> Get(int roomId, string lastUpdated)
        {
            try
            {
                DateTime dateLasteUpdated = DateTime.Parse(lastUpdated);
                var messages = await _messageRepository.GetLatestMessagesFromRoomAsync(roomId, dateLasteUpdated);
                return new OkObjectResult(messages);

            }
            catch (RoomNotFoundException rnf)
            {
                return new BadRequestObjectResult(rnf.Message); 
            }
        }


        [HttpPost]
        public  async Task<IActionResult> Post(Message message)
        {
            try
            {
                await _messageRepository.AddMessageAsync(message);
                return Ok();
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidMessageException)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
