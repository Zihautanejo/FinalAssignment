using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaRuleController:ControllerBase
    {
        private readonly EvaDbContext ruleDb;
        private readonly EvaRuleService evaruleService;

        public EvaRuleController(EvaDbContext ruleDb)
        {
            this.ruleDb = ruleDb;
            evaruleService = new EvaRuleService(ruleDb);
        }

        [HttpGet]
        public ActionResult<EvaRule> Get(string academy,string grade)
        {
            try
            {
                return evaruleService.GetRule(academy, grade);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
