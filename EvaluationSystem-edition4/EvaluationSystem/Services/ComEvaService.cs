using EvaluationSystem.DAO;
using EvaluationSystem.Models;

namespace EvaluationSystem.Services
{
    public class ComEvaService
    {
        ComEvaDbContext DbContext;
        public ComEvaService(ComEvaDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public void AddComEvalution(ComEvaluation evaluation)
        {
            DbContext.ComEvaluations.Add(evaluation);
            DbContext.SaveChanges();
        }
    }
}
