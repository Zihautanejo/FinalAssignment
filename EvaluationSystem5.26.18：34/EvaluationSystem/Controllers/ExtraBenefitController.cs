using EvaluationSystem.Services;
using EvaluationSystem.Models;
using EvaluationSystem.DAO;
using Microsoft.AspNetCore.Mvc;
namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraBenefitController:ControllerBase
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
                if (benefitService.GetExtraBenefit(extraBenefit.ExtraBenefitId) == null)
                {
                    benefitService.AddExtraBenefit(extraBenefit);
                }
                else
                {
                    return BadRequest("该加分项已经存在！");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return extraBenefit;

        }

        //删除加分项
        [HttpDelete("{id}")]
        public ActionResult DeleteBenefit(string id)
        {
            try
            {
                //级联删除用户信息表里的信息
                benefitService.RemoveExtraBenefit(id);
               

            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //修改加分项
        [HttpPut("{id}")]
        public ActionResult<ExtraBenefit> ModifyExtraBenefit(string id, ExtraBenefit extraBenefit)
        {
            if (id != extraBenefit.ExtraBenefitId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                benefitService.ModifyExtraBenefit(extraBenefit);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //查询
        [HttpGet("{id}")]
        public ActionResult<ExtraBenefit> GetBenefit(string id)
        {
            ExtraBenefit extraBenefit;
            try
            {
                extraBenefit=benefitService.GetExtraBenefit(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
            return extraBenefit;
        }
        [HttpGet]
        public ActionResult<List<ExtraBenefit>> GetAll()
        {
            return benefitService.QueryAll();
            
        }

        //根据类型
        [HttpGet("type")]
        public ActionResult<List<ExtraBenefit>> GetBenefitsByType(string type)
        {

            try
            {
                var benefits = benefitService.GetExtraBenefits(type);
                if (benefits != null)
                {
                    return benefits;
                }
                else
                {
                    return BadRequest("未找到符合要求的加分项！");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //根据状态
        [HttpGet("status")]
        public ActionResult<List<ExtraBenefit>> GetBenefitsByStatus(int status)
        {

            try
            {
                var benefits = benefitService.GetExtraBenefits(status);
                if (benefits != null)
                {
                    return benefits;
                }
                else
                {
                    return BadRequest("未找到符合要求的加分项！");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }






    }
}
