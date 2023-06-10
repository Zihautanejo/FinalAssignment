using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

      

        //发送人ID
      
        public string SenderID { get; set; }

        //发送人姓名
        public string SenderName { get; set; }

        //接收人ID

        public string  ReceiverID { get; set; }

        //发送时间
        public string  Time { get; set; }

        //内容
        public string Content { get; set; }

        

        //消息是否查看
        public bool IsChecked { set; get; }





        public Message(string SenderID, string SenderName,string time,string ReceiverID,  string content)
        {
            this.SenderID = SenderID;
            this.SenderName = SenderName;
            this.Time = time;
            this.ReceiverID = ReceiverID;
           
            this.Content = content;
            this.IsChecked = false;
        }

        public Message() { }


    }
}
