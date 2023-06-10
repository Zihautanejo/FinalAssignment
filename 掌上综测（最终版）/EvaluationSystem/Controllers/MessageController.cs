using EvaluationSystem.Services;
using EvaluationSystem.Models;
using EvaluationSystem.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly EvaDbContext messageDb;
        private readonly MessageService messageService;
       
        public MessageController(EvaDbContext messageDb)
        {
            this.messageDb = messageDb;
            messageService = new MessageService(messageDb);
        }

      

        [HttpPost]
        //增加消息
        public ActionResult<Message> Add(Message message)
        {
            try
            {
                messageService.AddMessage(message);
               
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return message;
        }

        [HttpDelete("{id}")]
        //删除消息
        public ActionResult Delete(int id)
        {
            try
            {
                messageService.RemoveMessage(id);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
           
        }

        //查询消息
        [HttpGet("receiver")]
        public ActionResult<List<Message>> GetBenefits(string id)
        {
            try
            {
                return messageService.GetMessages(id);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
           
        }
        //是否有消息未被查看
        [HttpGet("checked")]
        public ActionResult<bool> IscheckedMessage(string id) 
        {
            try
            {
                return messageService.IscheckedMessage(id);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        //修改消息
        [HttpPut]
        public ActionResult Put(Message message)
        {
            try
            {
                messageService.ModifyMessage(message);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

    }
}
