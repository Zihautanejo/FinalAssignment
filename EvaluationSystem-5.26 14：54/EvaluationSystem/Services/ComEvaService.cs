using System.Linq;
using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Services
{
    public class ComEvaService
    {
        EvaDbContext DbContext;
        public ComEvaService(EvaDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        /*public void AddComEvalution(ComEvaluation evaluation)
        {
            DbContext.ComEvaluations.Add(evaluation);
            DbContext.SaveChanges();
        }
        public ComEvaluation QueryComEvaByID(string id)
        {
            return DbContext.ComEvaluations
                //.Include(c=>c.A)
                .Include(c=>c.B1)
                .Include(c=>c.B2)
                .Include(c=>c.F3)
                .SingleOrDefault(c => c.ComEvaId == id);
        }

        public void DeleteComEvalution(string id)
        {
            var com = DbContext.ComEvaluations
                .Include(c => c.A)
                .SingleOrDefault(c => c.ComEvaId == id);
            if (com == null) return;
            //DbContext.comEvaAns.RemoveRange(com.A);
            DbContext.ComEvaluations.Remove(com);
            DbContext.SaveChanges();
        }*/
    }
}
