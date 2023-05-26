using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.ComponentModel;
using OfficeOpenXml;
using System.IO;
using System.Linq;

namespace EvaluationSystem.Services
{
    public interface IStudentCourseMarkService
    {
        void ImportStudentCourseMark(IFormFile file);
        List<StudentCourseMark> GetStudentCourseMark(string userId, string semester);
        List<StudentCourseMark> QueryStudentCourseMarkById(int Id);
    }
    public class StudentCourseMarkService: IStudentCourseMarkService
    {
        private readonly IStudentCourseMarkService _scmarkDao;

        public StudentCourseMarkService(IStudentCourseMarkDao scmarkDao)
        {
            _scmarkDao = (IStudentCourseMarkService?)scmarkDao;
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
            var grades = _scmarkDao.GetStudentCourseMark(userId, semester);
            return grades;
        }

        public List<StudentCourseMark> QueryStudentCourseMarkById(int Id)
        {
            var grades = _scmarkDao.QueryStudentCourseMarkById(Id);
            return grades;
        }
    }

}

    


