using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class ExtraBenefit
    {
      
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExtraBenefitId { get; set; }
        //导航属性
        [ForeignKey("ComEvaId")]
        public ComEvaluation comeva = new ComEvaluation();
        //加分主题
        public string BenefitTheme { get; set; }
        //加分类型
        public string BenefitType { get; set; }
        //奖励分数
        public string BenefitPoint { get; set; }
        //奖项时间
        public string BenefitTime { get; set; }

        //最近一次修改时间
        public string UpdateTime { get; set; }
        //加分申请状态 0待审核 1已通过 2已驳回 3已取消
        public string BenefitStatus { get; set; }
        //备注
        public string BenefitNote { get; set; }
        //证明材料
        public string ProofFile { get; set; }
        //是否在有效时间内
        public bool IsValidForThisSemester { get; set; }
       
        public string StuId { get; set; }
        public string StudentName { get; set; }
       
        public string TutorId { get; set; }

        public ExtraBenefit()
        {

        }
        public ExtraBenefit(string tutorid,string updatetime,string stuid,string name,string theme,string type,string point,string time,string status,string note,string ProofFile,bool isvalid)
        {
            this.UpdateTime = updatetime;
            this.TutorId =tutorid;
            this.StuId = stuid;
            this.StudentName = name;
            this.BenefitTheme = theme;
            this.BenefitType = type;
            this.BenefitPoint = point;
            this.BenefitTime = time;
            this.BenefitStatus = status;
            this.BenefitNote = note;
            this.ProofFile = ProofFile;
            this.IsValidForThisSemester = isvalid;
        }
        
    }
}

