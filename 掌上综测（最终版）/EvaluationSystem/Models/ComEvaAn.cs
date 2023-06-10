using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class ComEvaAn
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string  ComEvaAnId { get; set; }//自增主键

        //[ForeignKey("ComEvaId")]
        public ComEvaluation comeva;

        //MaxScore代表F1分数中取值的上限
        //Aself代表自查分，Aaudit代表审核分，Astring代表审核意见
        public double MaxScore = 20;
        private string Aself;
        private string Aaudit;
        public string ARemark { get; set; }
        public string ASelf
        {
            get { return Aself; }
            set { if (Convert.ToDouble(value) >= 0 && Convert.ToDouble(value) <= MaxScore) { Aself = value; } }
        }
        public string AAudit
        {
            get { return Aaudit; }
            set { if (Convert.ToDouble(value) >= 0 && Convert.ToDouble(value) <= MaxScore) { Aaudit = value; } }
        }
        public ComEvaAn() {
           // ComEvaAnId= Guid.NewGuid().ToString();
        }
        public ComEvaAn(double MaxScore=20, ComEvaluation com=null,string AAudit= "0", string ASelf= "0", string ARemark="")
        {
            //ComEvaAnId = Guid.NewGuid().ToString();
            this.MaxScore = MaxScore;
            this.AAudit = AAudit;
            this.ASelf = ASelf;
            this.ARemark = ARemark;
            this.comeva = com;
        }
    }
}