using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComEvaController : ControllerBase
    {
        private readonly DAO.EvaDbContext comevaDb;
        private readonly ComEvaService comEvaService;
        private readonly UserService userService;
        public ComEvaController(DAO.EvaDbContext comevaDb)
        {
            this.comevaDb = comevaDb;
            comEvaService = new ComEvaService(comevaDb);
        }

        //新建综测表
        [HttpPost]
        public ActionResult<ComEvaluation> PostComEva(ComEvaluation com)
        {
            try
            {
                comEvaService.AddComEvalution(com);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return com;
        }

        //学生页面

        //添加综测表

        //根据学生和学期查找
        [HttpGet("{StudentId}/{Semester}")]
        public ActionResult<List<ComEvaluation>> GetComEvaByStudent(string StudentId, string? Semester)
        {
            var com = comEvaService.QueryComEvaByStudent(StudentId, Semester);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //删除综测表
        
        //提交综测表


        //辅导员页面
        //总表
        //
        [HttpGet("{tutorid}/{sortid}/{state}")]
        public ActionResult<List<ComEvaluation>> QueryComEvaByStateOrderId(string grade,string major,string tutorid,int? state)
        {
            List<string> stuId = userService.QueryStuInfo(grade,major, tutorid,"*");
            List<ComEvaluation> comList= comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByState(comList,state);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }


    }
}
