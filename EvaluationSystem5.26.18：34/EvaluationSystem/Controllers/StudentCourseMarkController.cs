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
        List<StudentMark> stumark;

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
        public IActionResult GetStudentCourseMark(string userId, string grade,string semester)
        {
            // 调用service层获取课程成绩
            var grades = _scmark.GetStudentCourseMark(userId, grade,semester);

            return Ok(grades);
        }

        [HttpGet("queryGradesById")]
        public IActionResult QueryStudentCourseMarkById(int Id)
        {
            // 调用service层获取课程成绩
            var grades = _scmark.QueryStudentCourseMarkById(Id);

            return Ok(grades);
        }

        //根据学号获取该生所有的成绩单
        [HttpGet("queryStudentMarkByUserId")]
        public List<StudentMark> QueryStudentMarkByUserId(string userId)
        {
            string[] gradeList = { "大一", "大二", "大三", "大四" };
            string[] semesterList = { "第三学期","上学期", "下学期"};
            
            for(int i =1; i < 12; i++)
            {
                stumark[i-1]=_scmark.CreateStudentMarkEachSemester(userId, gradeList[i/4], semesterList[i%3]);
            }
            return stumark;
        }
    }
}