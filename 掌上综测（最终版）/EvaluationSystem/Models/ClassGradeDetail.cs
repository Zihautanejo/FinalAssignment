namespace EvaluationSystem.Models
{
    public class ClassGradeDetail
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double GPA { get; set; }

        public ClassGradeDetail(string userId, string userName, double gPA)
        {
            UserId = userId;
            UserName = userName;
            GPA = gPA;
        }
    }
}
