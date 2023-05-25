using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.DAO
{
    public class ComEvaDbContext:DbContext
    {
        public ComEvaDbContext(DbContextOptions<ComEvaDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ComEvaluation> ComEvaluations { get; set; } = null!;
        public DbSet<ExtraBenefit> extraBenefits { get; set; } = null!;
        public DbSet<ComEvaAn> comEvaAns { get; set; } = null!;
        public DbSet<CourseMark> courseMarks { get; set; } = null!;
    }
}

