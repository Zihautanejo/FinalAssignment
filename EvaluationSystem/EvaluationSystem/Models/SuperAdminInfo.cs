namespace EvaluationSystem.Models
{
    public class SuperAdminInfo
    {
        public string SuperAdminId { get; set; }
        public string PwdAnswer { get; set; }
        public string PwdQues { get; set; }
        public SuperAdminInfo(string UserId, string PwdAnswer, string PwdQues)
        {
            this.SuperAdminId = UserId;
            this.PwdAnswer = PwdAnswer;
            this.PwdQues = PwdQues;
        }

       
        public SuperAdminInfo() { }
    }
}
