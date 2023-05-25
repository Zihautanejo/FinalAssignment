using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class SuperAdminInfo
    {
        [Key]
        public string UserId { get; set; }
        public string PwdAnswer { get; set; }
        public string PwdQues { get; set; }
        public SuperAdminInfo(string UserId, string PwdAnswer, string PwdQues)
        {
            this.UserId = UserId;
            this.PwdAnswer = PwdAnswer;
            this.PwdQues = PwdQues;
        }

       
        public SuperAdminInfo() { }
    }
}
