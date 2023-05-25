using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystem.Models
{
    public struct An
    {
        //MaxScore代表F1分数中取值的上限
        //Aself代表自查分，Aaudit代表审核分，Astring代表审核意见
        public double MaxScore;
        private double Aself;
        private double Aaudit;
        public string ARemark;
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
    }

    public struct Bn
    {
        public int num;
        public string Bcoursename;
        private double Bgrade;
        private double Bcredit;
        public bool BState;//true表示正常，false表示不及格
        public double BGrade
        {
            get { return Bgrade; }
            set { if (value >= 0 && value <= 100) Bgrade = value; }
        }
        public double BCredit
        {
            get { return Bcredit; }
            set { if (value >= 0 && value <= 5) Bcredit = value; }
        }
    }

    public class ComEvaluation
    {
        //综测表的用户信息
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ComEvaId;//自增主键
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
        public List<An> A = new List<An>();
        //F1总分只能读不能写
        public double F1Self { get { return A.Sum(a => a.ASelf); } }
        public double F1Audit { get { return A.Sum(a => a.AAudit); } }

        //F2项-------------------------------------------------------
        public int F2B1num;
        public int F2B2num;
        public List<Bn> B1 = new List<Bn>();
        //public List<Bn> FailedB1 = B1.FindAll(b => b.BState = false);
        //注意F2B2的数目限制
        public List<Bn> AllB2 = new List<Bn>();
        public List<Bn> B2 = new List<Bn>();
        public double F2B1 { get { return (B1.Sum(b => b.BCredit * b.BGrade)) / (B1.Sum(b => b.BCredit)); } }
        public double F2B2 { get { return 0.002 * (B2.Sum(b => b.BCredit * b.BGrade)); } }
        public double F2 { get { return F2B1 + F2B2; } }

        //F3---------------------------------------------------------
        public int F3num;
        public double F3C1MaxScore;
        public double F3C2MaxScore;
        public double F3C3MaxScore;
        public double F3C4MaxScore;
        public double F3C5MaxScore;
        public List<string> ExtBenList = new List<string>();//存放的是通过的加分申明id号
        //注意各项分数的限制
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
        public ComEvaluation(string UID,string CID,int S) 
        {
            UserId = UID;
            ClassId = CID;
            Semester = S;
        }
        public ComEvaluation() { }
        public void Intialize(int F2n1, int F3C1,int F3C2,int F3C3, int F3C4,int F3C5,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)//初始化各项的数目
        {
            F1num = F1n;
            F1MaxScore = 100 / F1num;
            F2B1num = F2n1;
            F2B2num = F2n2;
            F3num = F3n;
        }
        //各个分数的限制，用函数来做
        
    }
}
