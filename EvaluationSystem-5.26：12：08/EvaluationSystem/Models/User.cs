
namespace EvaluationSystem.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string Sex { get; set; }

        //用户类型
        public int Type { get; set; }

        public User(string UserId,string Name,string Pwd,string Sex,int Type) 
        {
            this.UserId = UserId;
            this.Name = Name;
            this.Password = Pwd;
            this.Sex = Sex;
            this.Type = Type;
        }
        public User() 
        {
            
        }

        
    }
}
