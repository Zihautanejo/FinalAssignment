using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComEvaController:ControllerBase
    {
        private readonly DAO.EvaDbContext comevaDb;
        private readonly ComEvaService comEvaService;
        public ComEvaController(DAO.EvaDbContext comevaDb)
        {
            this.comevaDb = comevaDb;
            comEvaService = new ComEvaService(comevaDb);
        }

        //新建综测表
        /*[HttpPost]
        public ActionResult<ComEvaluation> PostComEva(ComEvaluation com)
        {
            try
            {
                comEvaService.AddComEvalution(com);
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return com;
        }*/
    }
    
    

}
