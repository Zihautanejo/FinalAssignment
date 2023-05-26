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
        public DbSet<StudentCourseMark> StudentCourseMark { get; set; } = null!;

    } 
    
}
