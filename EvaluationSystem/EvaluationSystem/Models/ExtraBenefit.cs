using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class ExtraBenefit
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string ExtBenId;
        public string UserId;
        public string benefitTheme; 
        public int benefittype;
        private double benefitgrade;
        public string benefitTime;
        public int benefitStatus;
        public string proofFile;//什么格式比较合适
        //证明材料的时效性从综测表的时间范围确定
       
        public double BenefitGrade
        {
            get { return benefitgrade; }
            set { if (value > 0) benefitgrade = value; }
        }
        public ExtraBenefit(string UID)
        {
            UserId = UID;
        }
        public ExtraBenefit() { }
    }
}
