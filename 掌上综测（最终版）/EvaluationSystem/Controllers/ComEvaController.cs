using System.Collections.Generic;
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
        private readonly StudentCourseMarkService studentCourseMarkService;
       

        public ComEvaController(DAO.EvaDbContext comevaDb)
        {
            this.comevaDb = comevaDb;
            comEvaService = new ComEvaService(comevaDb);
            userService = new UserService(comevaDb);
            classService = new ClassService(comevaDb);
            studentCourseMarkService = new StudentCourseMarkService(comevaDb);
            
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

        //辅导员页面，添加综测表
        [HttpPost("All")]
        public ActionResult<List<ComEvaluation>> PostAll(string major, string grade, string tutorid, string nowgrade,
                             double F3C1 = 10, double F3C2 = 10, double F3C3 = 10, double F3C4 = 10, double F3C5 = 10,
                              double F2B2rate = 0.002, double F1rate = 0.10, double F2rate = 0.7, double F3rate = 0.20,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)
        {
            return comEvaService.AddComEvalutionAll(major, grade, tutorid, nowgrade, F3C1, F3C2, F3C3, F3C4, F3C5,
                               F2B2rate, F1rate, F2rate, F3rate,
                               F1n, F2n2, F3n);
        }

        //学生页面

        //根据学生信息添加综测表
        [HttpPost("StudentId/grade")]
        public ActionResult<ComEvaluation> PostComEvaByStuIdGrade(string studentId, string grade)
        {
            ComEvaluation com;
            try
            {
                com = comEvaService.AddComEvalutionByStuIfo(studentId, grade);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return com;
        }

        //根据学生和学期查找
        [HttpGet("StudentId")]
        public ActionResult<List<ComEvaluation>> GetComEvaByStudent(string StudentId, string grade)
        {
            var com = comEvaService.QueryComEvaByStudent(StudentId, grade);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        [HttpGet("ComEvaId")]
        public ActionResult<ComEvaluation> GetComEvaByComEvaId(string ComEvaId)
        {
            var com= comEvaService.QueryComEvaById(ComEvaId);
            return com;
            
        }

        //删除综测表

        //提交综测表


        //辅导员页面

        //总表 InGeneral
        //按照学号排序
        [HttpGet("tutorid/major/grade/Id")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderId(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderId(comEvaService.QueryComEvaByState(comList, "已过审"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //综测成绩升序
        [HttpGet("tutorid/major/grade/GradeUp")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderGradeUP(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderGradeUp(comEvaService.QueryComEvaByState(comList, "已过审"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //综测成绩降序
        [HttpGet("tutorid/major/grade/GradeDown")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInGeneralOrderGradeDown(string tutorid, string major, string grade)
        {
            List<string> stuId = userService.QueryStuInfo(grade, major, tutorid);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByList(stuId);
            var com = comEvaService.QueryComEvaByListOrderGradeDown(comEvaService.QueryComEvaByState(comList, "已过审"));
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //班级内表 InClass
        //学号排序
        [HttpGet("major/grade/num/state/word/Id")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderId(string grade, string major, int num, string state, string?word)
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
        [HttpGet("major/grade/num/state/word/GradeUp")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderGradeUp(string grade, string major, int num, string state, string? word)
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
        [HttpGet("major/grade/num/state/word/GradeDown")]
        public ActionResult<List<ComEvaluation>> QueryComEvaInClassOrderGradeDown(string grade, string major, int num, string state, string ?word)
        {
            CClass C = classService.GetCClassesByMajorGradeNum(major, grade, num);
            List<ComEvaluation> comList = comEvaService.QueryComEvaByClassId(C);
            var com = comEvaService.QueryComEvaByState(comList, state);
            com = comEvaService.QueryComEvaByword(com, word);
            com = comEvaService.QueryComEvaByListOrderGradeDown(com);
            if (com == null)
            {
                return NotFound();
            }
            else return com;
        }

        //年级间的班级排序InGrade
        //按照班号排序
        [HttpGet("tutorid/major/grade/IdUp")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderClassId(string tutorid, string? major, string? grade)
        {
            string s1 = (major == null ? "all" : major);
            string s2 = (grade == null ? "all" : grade);
            List<CClass> list = classService.GetCClassesByMajorGrade(s1, s2, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByClassNum(major, grade);
            return list;
        }

        //提交人数升序
        [HttpGet("tutorid/major/grade/SubUp")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderSubUp(string tutorid, string? major, string? grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByNotSubNum(major, grade);
            return list;
        }

        //提交人数降序
        [HttpGet("tutorid/major/grade/SubDown")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderSubDown(string tutorid, string? major, string? grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByNotSubNumDescend(major, grade);
            return list;
        }

        //平均分升序
        [HttpGet("tutorid/major/grade/AVGUp")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderAVGUp(string tutorid, string? major, string? grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByAvaPoint(major, grade);
            return list;
        }

        //平均分降序
        [HttpGet("tutorid/major/grade/AVGDown")]
        public ActionResult<List<CClass>> QueryClassInGradeOrderAVGDown(string tutorid, string? major, string? grade)
        {
            List<CClass> list = classService.GetCClassesByMajorGrade(major, grade, null);
            comEvaService.ModifyClassIfo(list);
            list = classService.SortByAvaPointDescend(major, grade);
            return list;
        }
        //把自评导入综测表中
        [HttpPut("stuId/F1Self")]
        public IActionResult AddStudentF1(string comid, List<ComEvaAn> e)
        {

            ComEvaluation com = comEvaService.QueryComEvaById(comid);
            com = comEvaService.ModifyComEvaF1Self(com, e);
            if (com == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(com);
            }
        }
        //把审核导入综测表
        [HttpPut("stuId/F1Audit")]
        public IActionResult AddTutorF1(string comid, List<ComEvaAn> e)
        {

            ComEvaluation com = comEvaService.QueryComEvaById(comid);
            com = comEvaService.ModifyComEvaF1Audit(com, e);
            if (com == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(com);
            }
        }

        //把加分项导入到综测表中
        [HttpPut("stuId/extrabenefit")]
        public IActionResult AddStudentExtrabenefit(string StudentId, string grade, List<ExtraBenefit> e)
        {
            var com = comEvaService.AddBenefit(StudentId, grade, e);
            if (com == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(com);
            }
        }

        //把选修课导入到综测表中
        [HttpPut("stuId/addSelectiveCourse")]
        public IActionResult addSelectiveCourse(string StudentId, string grade, List<StudentCourseMark> selectedData)
        {

            var com = comEvaService.AddselectedData(StudentId, grade,selectedData);
            if (com == null)
            {
                return NotFound();
            }
            else { return Ok(com);} 

        }

        //把课程分数写入综测表中
        [HttpPut("stuId/addCourseScore")]
        public IActionResult addCourseScore(string StudentId, string grade, string semester,string comid)
        {
            ComEvaluation com = comEvaService.QueryComEvaById(comid);
            List<StudentCourseMark> sm = studentCourseMarkService.GetStudentCourseMark(StudentId, grade, "all", "专业必修课", null);
            List<StudentCourseMark> sn = studentCourseMarkService.GetStudentCourseMark(StudentId, grade, "all", "专业选修课", null);
            com = comEvaService.ModifyComEvaF2Score(com,sm,sn);
            if (com == null)
            {
                return NotFound();
            }
            else { return Ok(com); }

        }

        //把必修课导入到综测表中
        [HttpPut("stuId/addRequiredCourse")]
        public IActionResult addRequiredCourse(string StudentId, string grade, List<StudentCourseMark> RequiredData)
        {

            var com = comEvaService.AddrequiredData(StudentId, grade, RequiredData);
            if (com == null)
            {
                return NotFound();
            }
            else { return Ok(com); }

        }

        //修改综测表状态
        [HttpPut("ModifyState/Submit")]
        public ActionResult<ComEvaluation> ModifyStateSubmit(string ComEvaId)
        {
            var com=comEvaService.ModifyStateSubmit(ComEvaId);
            return com;
        }

        [HttpPut("ModifyState/Success")]
        public ActionResult<ComEvaluation> ModifyStateSucess(string ComEvaId)
        {
            var com= comEvaService.ModifyStateSucess(ComEvaId);
            return com;
        }

        [HttpPut("ModifyState/Fail")]
        public ActionResult<ComEvaluation> ModifyStateFail(string ComEvaId)
        {
            var com=comEvaService.ModifyStateFail(ComEvaId);
            return com;
        }
    }
}

