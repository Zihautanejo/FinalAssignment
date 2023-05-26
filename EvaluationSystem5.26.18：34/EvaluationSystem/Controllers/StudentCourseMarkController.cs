using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseMarkController : ControllerBase
    {
        private readonly StudentCourseMarkService _scmark;

        public StudentCourseMarkController(StudentCourseMarkService scmark)
        {
            _scmark = scmark;
        }

        [HttpPost("importGrades")]
        public IActionResult ImportStudentCourseMark(IFormFile file)
        {
            // 调用service层导入课程成绩
            _scmark.ImportStudentCourseMark(file);

            // 返回成功信息
            return Ok();
        }

        [HttpGet("getGrades")]
        public IActionResult GetStudentCourseMark(string userId, string semester)
        {
            // 调用service层获取课程成绩
            var grades = _scmark.GetStudentCourseMark(userId, semester);

            return Ok(grades);
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