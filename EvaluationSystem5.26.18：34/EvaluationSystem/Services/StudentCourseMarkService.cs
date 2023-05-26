using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.ComponentModel;
//using OfficeOpenXml;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Services
{
    public interface IStudentCourseMarkService
    {
        void ImportStudentCourseMark(IFormFile file);
        List<StudentCourseMark> GetStudentCourseMark(string userId, string semester);
        List<StudentCourseMark> QueryStudentCourseMarkById(int Id);
        void SaveStudentCourseMark(List<StudentCourseMark> scmark);


    }
    public class StudentCourseMarkService : IStudentCourseMarkService
    {
        private readonly IStudentCourseMarkService _scmarkDao;
        EvaDbContext _dbContext;

        public StudentCourseMarkService(EvaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ImportStudentCourseMark(IFormFile file)
        {
            /*
            //读取Excel文件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage(stream);
            var workSheet = excel.Workbook.Worksheets.First();

            //读取数据
            List<StudentCourseMark> studentCourseMarks = new List<StudentCourseMark>();
            for (int row = 2; row <= workSheet.Dimension.End.Row; row++)
            {
                StudentCourseMark studentCourseMark = new StudentCourseMark(
                    courseName: workSheet.Cells[row, 1].Value.ToString(),
                    courseType: workSheet.Cells[row, 2].Value.ToString(),
                    coursePoint: double.Parse(workSheet.Cells[row, 3].Value.ToString()),
                    courseId: workSheet.Cells[row, 4].Value.ToString(),
                    UserId: workSheet.Cells[row, 5].Value.ToString(),
                    stuMark: int.Parse(workSheet.Cells[row, 6].Value.ToString()),
                    courseTakeSemester: workSheet.Cells[row, 7].Value.ToString(),
                    isFailed: bool.Parse(workSheet.Cells[row, 8].Value.ToString()),
                    isRetook: bool.Parse(workSheet.Cells[row, 9].Value.ToString()),
                    stuGradePoint: int.Parse(workSheet.Cells[row, 10].Value.ToString())
                );
                studentCourseMarks.Add(studentCourseMark);
            }

            _scmarkDao.SaveStudentCourseMark(studentCourseMarks);
            */
        }
        public List<StudentCourseMark> GetStudentCourseMark(string userId, string semester)
        {
            var scmark = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.courseTakeSemester == semester);
            return scmark.ToList();
        }

        public List<StudentCourseMark> QueryStudentCourseMarkById(int Id)
        {
            var scmark = _dbContext.StudentCourseMark.Where(x => x.Id == Id);
            return scmark.ToList();
        }

        public void SaveStudentCourseMark(List<StudentCourseMark> scmark)
        {
            // 保存成绩到数据库
            _dbContext.StudentCourseMark.AddRange(scmark);
            _dbContext.SaveChanges();
        }
    }

}
