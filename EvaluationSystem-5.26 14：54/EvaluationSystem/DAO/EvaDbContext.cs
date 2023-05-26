
using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace EvaluationSystem.DAO

{

    public class EvaDbContext : DbContext
    {
        public EvaDbContext(DbContextOptions<EvaDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }

        //用户
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<StudentInfo> Students { get; set; } = null!;

        public DbSet<TutorInfo> Tutors { get; set; } = null!;

        public DbSet<SuperAdminInfo> SuperAdmins { get; set; } = null!;

        //额外加分数据库
        public DbSet<ExtraBenefit> extraBenefits { get; set; } = null!;
        
        //综测表数据库
        //public DbSet<ComEvaluation> ComEvaluations { get; set; } = null!;
        //public DbSet<ComEvaAn> comEvaAns { get; set; } = null!;
        //成绩单数据库
        public DbSet<StudentCourseMark> StudentCourseMark { get; set; }=null!;


       

    }
}
