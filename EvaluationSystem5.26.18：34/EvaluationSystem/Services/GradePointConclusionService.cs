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

        public double CalculateGPA(string userId)
        {
            double GPA=0;
            //待补充
            return GPA;
        }
    }
}
