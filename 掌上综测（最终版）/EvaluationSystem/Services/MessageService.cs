using EvaluationSystem.DAO;
using EvaluationSystem.Models;

namespace EvaluationSystem.Services
{
    public class MessageService
    {

        EvaDbContext dbContext;
        public MessageService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //添加信息
        public void AddMessage(Message message)
        {
            dbContext.messages.Add(message);
            dbContext.SaveChanges();
        }

        //删除信息
        public void RemoveMessage(int id)
        {
            var message = dbContext.messages.FirstOrDefault(x => x.MessageID == id);
            if (message != null)
            {
                dbContext.messages.Remove(message);
                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("不存在该信息！");
            }
        }

        //修改信息
        public void ModifyMessage(Message m)
        {
           RemoveMessage(m.MessageID);
            AddMessage(m);
            
        }

        //根据接收者ID查找信息
        public List<Message> GetMessages(string id)
        {
            var query=dbContext.messages
                .Where(x => x.ReceiverID == id);
            return query.ToList();
        }

        //查看是否有未查收消息
        public bool IscheckedMessage(string id)
        {
            var query= dbContext.messages
                .Where (x => x.IsChecked==false&& x.ReceiverID == id);
            if(query.Count()!=0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        
    }
}
