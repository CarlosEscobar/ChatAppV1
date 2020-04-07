using ChatAppV1.Bot;
using ChatAppV1.DataAccess.Entities;
using ChatAppV1.DataAccess.UnitOfWork;
using ChatAppV1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatAppV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpClientFactory clientFactory;

        public MessagesController(IUnitOfWork unitOfWork, IHttpClientFactory clientFactory)
        {
            this.unitOfWork = unitOfWork;
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public ActionResult GetMessages()
        {
            try
            {
                return Ok(unitOfWork.MessageRepository.GetAll().Result.OrderByDescending(m => m.Timestamp).Take(50));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddMessage([FromBody] MessageModel message)
        {
            try
            {
                if (message.Content.StartsWith("/stock="))
                {
                    message.Owner = "ChatAppBot";
                    message.Content = await new ChatAppBot(clientFactory.CreateClient(), message.Content.Substring(7)).ProcessStockCode();
                }
                unitOfWork.MessageRepository.Create(new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    SocketId = message.SocketId,
                    Owner = message.Owner,
                    Content = message.Content,
                    Timestamp = DateTime.Now
                });
                await unitOfWork.Commit();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}