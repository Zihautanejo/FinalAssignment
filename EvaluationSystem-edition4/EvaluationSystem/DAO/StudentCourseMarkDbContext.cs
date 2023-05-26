using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EvaluationSystem.DAO
{
    public class StudentCourseMarkDbContext : DbContext
    {
        public StudentCourseMarkDbContext(DbContextOptions<StudentCourseMarkDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }
        public DbSet<StudentCourseMark> StudentCourseMark { get; set; }
      
    }
    public interface IStudentCourseMarkDao
    {
        void SaveStudentCourseMark(List<StudentCourseMark> scmark);
        List<StudentCourseMark> GetStudentCourseMark(string userId, string semester);
    }

    public class StudentCourseMarkDao : IStudentCourseMarkDao
    {
        private readonly StudentCourseMarkDbContext _dbContext;

        public StudentCourseMarkDao(StudentCourseMarkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveStudentCourseMark(List<StudentCourseMark> scmark)
        {
            // 保存成绩到数据库
            _dbContext.StudentCourseMark.AddRange(scmark);
            _dbContext.SaveChanges();
        }

        public List<StudentCourseMark> GetStudentCourseMark(string userId, string semester)
        {
            //从数据库获取成绩
            var scmark = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.courseTakeSemester == semester) ;
            return scmark.ToList();
        }
    }
}
