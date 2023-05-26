using EvaluationSystem.Services;
using EvaluationSystem.Models;
using EvaluationSystem.DAO;
using Microsoft.AspNetCore.Mvc;
namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraBenefitControllrer:ControllerBase
    {
        
        private readonly DAO.EvaDbContext benefitDb;
        private readonly ExtraBenefitService benefitService;

        public ExtraBenefitControllrer(DAO.EvaDbContext benefitDb)
        {
            this.benefitDb = benefitDb;
            benefitService = new ExtraBenefitService(benefitDb);
        }

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




    }
}
