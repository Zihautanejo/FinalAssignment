using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class ComEvaAn
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ComEvaAnId { get; set; }//自增主键
        
        //MaxScore代表F1分数中取值的上限
        //Aself代表自查分，Aaudit代表审核分，Astring代表审核意见
        public double MaxScore { get; set; }
        private double Aself;
        private double Aaudit;
        public string ARemark { get; set; }
        public double ASelf
        {
            get { return Aself; }
            set { if (value >= 0 && value <= MaxScore) { Aself = value; } }
        }
        public double AAudit
        {
            get { return Aaudit; }
            set { if (value >= 0 && value <= MaxScore) { Aaudit = value; } }
        }
        public ComEvaAn() { }
        public ComEvaAn(double MaxScore, double AAudit,double ASelf,string ARemark)
        {
            this.MaxScore = MaxScore;
            this.AAudit = AAudit;
            this.ASelf = ASelf;
            this.ARemark = ARemark;
        }
    }
}
