using System.Diagnostics;
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
        private readonly ClassService classService;

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

        //总表 InGeneral
        //按照学号排序
        [HttpGet("{tutorid}/{major}/{grade}/Id")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderId(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderId(comEvaService.QueryComEvaByState(comList, "2"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //综测成绩升序
        [HttpGet("{tutorid}/{major}/{grade}/GradeUp")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderGradeUP(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderGradeUp(comEvaService.QueryComEvaByState(comList, "2"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //综测成绩降序
        [HttpGet("{tutorid}/{major}/{grade}/GradeDown")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderGradeDown(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderGradeDown(comEvaService.QueryComEvaByState(comList, "2"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //班级内表 InClass
        //学号排序
        [HttpGet("{major}/{grade}/{num}/{state}/{word}/Id")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderId(string grade,string major,int num, string state,string word)
        {
            CClass C = classService.GetCClassesByMajorGradeNum(major, grade, num);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByClassId(C);
            var com = comEvaService.QueryComEvaByState(comList, state);
            com = comEvaService.QueryComEvaByword(com, word);
            com = comEvaService.QueryComEvaByListOrderId(com);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //成绩升序
        [HttpGet("{major}/{grade}/{num}/{state}/{word}/GradeUp")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderGradeUp(string grade, string major, int num, string state,string word)
        {
            CClass C = classService.GetCClassesByMajorGradeNum(major, grade, num);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByClassId(C);
            var com = comEvaService.QueryComEvaByState(comList, state);
            com = comEvaService.QueryComEvaByword(com, word);
            com = comEvaService.QueryComEvaByListOrderGradeUp(com);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //成绩降序
        [HttpGet("{major}/{grade}/{num}/{state}/{word}/GradeDown")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderGradeDown(string grade, string major, int num, string state,string word)
        {
            CClass C = classService.GetCClassesByMajorGradeNum(major, grade, num);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByClassId(C);
            var com = comEvaService.QueryComEvaByState(comList, state);
            com = comEvaService.QueryComEvaByword(com,word);
            com = comEvaService.QueryComEvaByListOrderGradeDown(com);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //InGrade
        //按照班号排序
        [HttpGet("{tutorid}/{major}/{grade}/Id")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderClassId(string tutorid,string major,string grade)
        {
            List<CClass> list=classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list=classService.SortByClassNum(major, grade);
            return list;
        }

        //提交人数升序
        [HttpGet("{tutorid}/{major}/{grade}")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderSubUp(string tutorid, string major, string grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByNotSubNum(major, grade);
            return list;
        }

        //提交人数降序
        [HttpGet("{tutorid}/{major}/{grade}")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderSubDown(string tutorid, string major, string grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByNotSubNumDescend(major, grade);
            return list;
        }

        //平均分升序
        [HttpGet("{tutorid}/{major}/{grade}")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderAVGUp(string tutorid, string major, string grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByAvaPoint(major, grade);
            return list;
        }

        //平均分降序
        [HttpGet("{tutorid}/{major}/{grade}")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderAVGDown(string tutorid, string major, string grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByAvaPointDescend(major, grade);
            return list;
        }
    }
}
