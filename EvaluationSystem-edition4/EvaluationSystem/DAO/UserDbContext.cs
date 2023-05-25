
using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace EvaluationSystem.DAO

{

    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<StudentInfo> Students { get; set; } = null!;

        public DbSet<TutorInfo> Tutors { get; set; } = null!;

        public DbSet<SuperAdminInfo> SuperAdmins { get; set; } = null!;

    }
}
