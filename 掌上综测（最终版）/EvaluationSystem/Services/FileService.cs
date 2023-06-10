using EvaluationSystem.DAO;
using EvaluationSystem.Models;

namespace EvaluationSystem.Services
{
   
        public interface IFileService
        {
            void importExcel(string filePath);
            //GradePointConclusion GetStudentGradePointConclusion(string userId, string EnrollmentYear, string major);
        }
        public class FileService : IFileService
        {
            private readonly IFileService _fileDao;
            EvaDbContext _dbContext;

            public FileService(EvaDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public void importExcel(string filePath)
            {

            }
        }
     
}
