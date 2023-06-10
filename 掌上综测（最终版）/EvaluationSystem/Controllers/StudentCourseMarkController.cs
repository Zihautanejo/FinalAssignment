using EvaluationSystem.Models;
using EvaluationSystem.Services;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseMarkController : ControllerBase
    {
        private readonly DAO.EvaDbContext comevaDb;
        private readonly StudentCourseMarkService _scmark;

        public StudentCourseMarkController(DAO.EvaDbContext comevaDb)
        {
            this.comevaDb = comevaDb;
            _scmark = new StudentCourseMarkService(comevaDb);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("importGrades")]
        public IActionResult ImportExcel(IFormFile file)
        {
            try
            {
                _scmark.ImportExcelData(file);
                return Ok("导入成功");
            }
            catch (Exception ex)
            {
                return BadRequest($"导入失败: {ex.Message}");
            }
        }

        [HttpGet("getGrades")]
        public IActionResult GetStudentCourseMark(string userId, string grade, string semester, string classType, string? input)
        {
            try
            {
                // 调用service层获取课程成绩
                List<StudentCourseMark> stucoursemarks = _scmark.GetStudentCourseMark(userId, grade, semester, classType, input);
                return Ok(stucoursemarks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("queryGradesById")]
        public IActionResult QueryStudentCourseMarkById(int Id)
        {
            // 调用service层获取课程成绩
            var grades = _scmark.QueryStudentCourseMarkById(Id);

            return Ok(grades);
        }
    }
}