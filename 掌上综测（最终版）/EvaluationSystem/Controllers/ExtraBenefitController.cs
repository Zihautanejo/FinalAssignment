using EvaluationSystem.Services;
using EvaluationSystem.Models;
using EvaluationSystem.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraBenefitController : ControllerBase
    {

        private readonly DAO.EvaDbContext benefitDb;
        private readonly ExtraBenefitService benefitService;

        public ExtraBenefitController(DAO.EvaDbContext benefitDb)
        {
            this.benefitDb = benefitDb;
            benefitService = new ExtraBenefitService(benefitDb);
        }

        //增加加分项
        [HttpPost]
        public ActionResult<ExtraBenefit> AddExtraBenefit(ExtraBenefit extraBenefit)
        {
            try
            {
               
                
                    benefitService.AddExtraBenefit(extraBenefit);
                
               
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return extraBenefit;

        }

        //删除加分项
        [HttpDelete("{id}")]
        public ActionResult DeleteBenefit(int id)
        {
            try
            {
                //级联删除用户信息表里的信息
                benefitService.RemoveExtraBenefit(id);


            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        //修改加分项
        [HttpPut("{id}")]
        public ActionResult<ExtraBenefit> ModifyExtraBenefit(int id, ExtraBenefit extraBenefit)
        {
            if (id != extraBenefit.ExtraBenefitId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                benefitService.ModifyExtraBenefit(extraBenefit);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        //查询
        [HttpGet("{id}")]
        public ActionResult<List<ExtraBenefit>> GetBenefit(string id)
        {

            try
            {
                return benefitService.GetExtraBenefit(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        //学生查询
        [HttpGet("query")]
        public ActionResult<List<ExtraBenefit>> GetBenefits( string id,string? type, string? status, string? Btheme)
        {
            try
            {
                return benefitService.GetBenefits(type, status, Btheme,id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //辅导员查询
        [HttpGet("query/tutor")]
        public ActionResult<List<ExtraBenefit>> GetBenefitsByTutor(string id,string?major,string?grade,string?status,string?type)
        {
            try
            {
                return benefitService.GetBenefitsTutor(major, grade, status, type, id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tutorid")]
        public ActionResult<List<ExtraBenefit>> GetBenefitsBytutor(string id)
        {
            try
            {
                return benefitService.QueryByTutor(id);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
       







    }
}
