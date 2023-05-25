using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class ComEvaluation
    {
        //综测表的用户信息
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ComEvaId;//自增主键
        [ForeignKey("UserId")]
        public string UserId;
        public string ClassId;
        public int Semester;
        public int State;//0为填写未提交，1为提交未审批，2为审批通过，3为审批未通过

        //综测表的基本信息
        //综测表的证明材料时间范围
        public string BeginTime;
        public string EndTimr;

        //F1项--------------------------------------------------------
        //F1num代表F1细分的数目，F1MaxScore代表F1分数中取值的上限                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        public int F1num;
        public double F1MaxScore = 20;
        public List<ComEvaAn> A { get; set; }
        //F1总分只能读不能写
        public double F1Self { get { return A.Sum(a => a.ASelf); } }
        public double F1Audit { get { return A.Sum(a => a.AAudit); } }

        //F2项-------------------------------------------------------
        public int F2B1num;
        public int F2B2num;
        public List<CourseMark> B1 { get; set; }
        //public List<Bn> FailedB1 = B1.FindAll(b => b.BState = false);
        public List<CourseMark> B2 { get; set; }
        public double F2B1;//{ get { return (B1.Sum(b => b.Credit * b.Grade)) / (B1.Sum(b => b.Credit)); } }
        public double F2B2;//{ get { return 0.002 * (B2.Sum(b => b.Credit * b.Grade)); } }
        public double F2 { get { return F2B1 + F2B2; } }

        //F3---------------------------------------------------------
        public int F3num;
        public double F3C1MaxScore;
        public double F3C2MaxScore;
        public double F3C3MaxScore;
        public double F3C4MaxScore;
        public double F3C5MaxScore;
        //注意各项分数的限制
        public List<ExtraBenefit> ExtBenList { get; set; }//存放的是通过的加分申明
        
        public double F3C1;
        public double F3C2;
        public double F3C3;
        public double F3C4;
        public double F3C5;
        public double F3 { get { return F3C1 + F3C2 + F3C3 + F3C4 + F3C5; } }

        //F
        public double FSelf { get { return 0.1 * F1Self + 0.75 * F2 + 0.15 * F3; } }
        public double FAudit { get { return 0.1 * F1Audit + 0.75 * F2 + 0.15 * F3; } }

        //构造函数，包含个人信息
        public ComEvaluation(string UID, string CID, int S)
        {
            UserId = UID;
            ClassId = CID;
            Semester = S;
            State = 0;
        }
        public ComEvaluation() { }
        public void Intialize(int F2n1, int F3C1, int F3C2, int F3C3, int F3C4, int F3C5,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)//初始化各项的数目
        {
            F1num = F1n; A = new List<ComEvaAn>(F1n);
            F1MaxScore = 100 / F1num;
            F2B1num = F2n1; B1 = new List<CourseMark>(F2n1);
            F2B2num = F2n2; B2 = new List<CourseMark>(F2n2);
            F3num = F3n;
        }
        //F1的填写及查看
        public void WriteF1Self(int i, double aself = 0, string aremark = null)
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].ASelf = aself;
            A[i].ARemark = aremark;
        }

        public void WriteF1Audit(int i, double aaudit)
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].AAudit = aaudit;
        }

        //F2的填写
        public void WriteF2B1(int num)
        {

        }

    }
}
