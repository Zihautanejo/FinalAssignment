using EvaluationSystem.DAO;

namespace EvaluationSystem.Services
{
    public class GradePointConclusionService
    {
        EvaDbContext dbContext;
        public GradePointConclusionService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
