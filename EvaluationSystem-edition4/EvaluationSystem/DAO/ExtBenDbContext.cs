using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.DAO
{
    public class ExtBenDbContext:DbContext
    {
        public ExtBenDbContext(DbContextOptions<ExtBenDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ComEvaluation> comEvaluations { get; set; } = null!;
    }
}
