using EvaluationSystem.Models;
using EvaluationSystem.Services;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradePointConclusionController : ControllerBase
    {
        private readonly DAO.EvaDbContext comevaDb;
        private readonly GradePointConclusionService _gpconclusion;

        public GradePointConclusionController(DAO.EvaDbContext comevaDb)
        {
            this.comevaDb = comevaDb;
            _gpconclusion = new GradePointConclusionService(comevaDb);
        }
        //获取一个学生的成绩分析
        [HttpGet("getGradesConclusion")]
        public IActionResult GetStudentGradePointConclusion(string userId, string EnrollmentYear, string major)
        {
            try
            {
                GradePointConclusion gpc = _gpconclusion.GetStudentGradePointConclusion(userId, EnrollmentYear, major);
                // 返回成功信息
                return Ok(gpc);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //辅导员获取一个班级的学生成绩分析
        [HttpGet("getStuGradesConcluInClass")]
        public IActionResult getStuGradesConcluInClass(string grade, string major, string classnum)
        {
            try
            {
                List<ClassGradeDetail> classGradeDetail = _gpconclusion.getStuGradesConcluInClass(grade, major, classnum);
                // 返回成功信息
                return Ok(classGradeDetail);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //辅导员获取一个班级的学生成绩分析的排序
        [HttpGet("sortClassGradeDetailByUserId")]
        public IActionResult sortClassGradeDetailByUserId(string grade, string major, string classnum)
        {
            try
            {
                List<ClassGradeDetail> classGradeDetail = _gpconclusion.sortClassGradeDetailByUserId(grade, major, classnum);
                return Ok(classGradeDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sortClassGradeDetailByGpaDesc")]
        public IActionResult sortClassGradeDetailByGpaDesc(string grade, string major, string classnum)
        {
            try
            {
                List<ClassGradeDetail> classGradeDetail = _gpconclusion.sortClassGradeDetailByGpaDesc(grade, major, classnum);
                return Ok(classGradeDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sortClassGradeDetailByGpa")]
        public IActionResult sortClassGradeDetailByGpa(string grade, string major, string classnum)
        {
            try
            {
                List<ClassGradeDetail> classGradeDetail = _gpconclusion.sortClassGradeDetailByGpa(grade, major, classnum);
                return Ok(classGradeDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("query")]
        public IActionResult query(string grade, string major, string? classnum, int rank, int status)
        {
            try
            {
                List<ClassGradeDetail> classGradeDetail = _gpconclusion.query(grade, major, classnum, rank, status);
                return Ok(classGradeDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("modifyAll")]
        public IActionResult modifyAll(string grade, string major, int? num)
        {
            try
            {
                List<int> i = _gpconclusion.modifyall(grade, major, num);
                return Ok(i);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("modifyPrecautionNum")]
        public IActionResult modifyPrecautionNum(string grade, string major, string num)
        {
            try
            {
                int i = _gpconclusion.modifyPrecautionNum(grade, major, num);
                return Ok(i);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getClassGradeConclusion")]
        public IActionResult getClassGradeConclusion(string grade, string major)
        {
            try
            {
                List<CClass> classconclu = _gpconclusion.getClassGradeConclusion(grade, major);
                return Ok(classconclu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
