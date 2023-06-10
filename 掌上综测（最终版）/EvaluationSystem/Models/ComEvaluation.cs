using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Mozilla;
using EvaluationSystem.Services;

namespace EvaluationSystem.Models
{
    public class ComEvaluation
    {
        [Key]
        //综测表的用户信息
        public string ComEvaId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public string Grade { get; set; }
        public string State { get; set; }//未填报，未审批，已过审，未过审

        public string Name { get; set; }
        public string Major { get; set; }
        public int ClassNum { get; set; }

        //综测表的基本信息
        //综测表的证明材料时间范围
        public string BeginTime { get; set; }
        public string EndTime { get; set; }

        //F1项--------------------------------------------------------
        //F1num代表F1细分的数目，F1MaxScore代表F1分数中取值的上限                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        public int F1num { get; set; }
        public double F1MaxScore { get => 100 / (F1num != 0 ? F1num : 1); }

        //[ForeignKey("ComEvaAnId")]
        public List<ComEvaAn> A { get; set; }
        //F1总分只能读不能写
        public double F1Self { get { return A.Sum(a => Convert.ToDouble(a.ASelf)); } }
        public double F1Audit { get { return A.Sum(a => Convert.ToDouble(a.AAudit)); } }

        //F2项-------------------------------------------------------
        public int F2B2num { get; set; }
        public double F2B2rate { get; set; }
        [NotMapped]
        public List<StudentCourseMark> B1 { get; set; }
        [NotMapped]
        public List<StudentCourseMark> B2 { get; set; }
        public double F2B1 { get { return (B1.Sum(b => b.stuMark * b.coursePoint)) / (B1.Sum(b => b.coursePoint) == 0 ? 1 : B1.Sum(b => b.coursePoint)); } }
        public double F2B2 { get { return F2B2rate * (B2.Sum(b => b.stuMark * b.coursePoint)); } }
        public double F2 { get { return F2B1 + F2B2; } }

        //F3---------------------------------------------------------
        public int F3num { get; set; }
        public double F3C1MaxScore { get; set; }
        public double F3C2MaxScore { get; set; }
        public double F3C3MaxScore { get; set; }
        public double F3C4MaxScore { get; set; }
        public double F3C5MaxScore { get; set; }
        //注意各项分数的限制
        //[ForeignKey("ExtraBenefitId")]
        public List<ExtraBenefit> ExtBenList { get; set; }//存放的是通过的加分申明

        public double F3C1
        {
            get
            {
                return Math.Min(F3C1MaxScore,
                ExtBenList.FindAll(c => c.BenefitType == "C1").Sum(c => Convert.ToDouble(c.BenefitPoint)));
            }
        }
        public double F3C2
        {
            get
            {
                return Math.Min(F3C2MaxScore,
                ExtBenList.FindAll(c => c.BenefitType == "C2").Sum(c => Convert.ToDouble(c.BenefitPoint)));
            }
        }
        public double F3C3
        {
            get
            {
                return Math.Min(F3C3MaxScore,
                ExtBenList.FindAll(c => c.BenefitType == "C3").Sum(c => Convert.ToDouble(c.BenefitPoint)));
            }
        }
        public double F3C4
        {
            get
            {
                return Math.Min(F3C4MaxScore,
                ExtBenList.FindAll(c => c.BenefitType == "C4").Sum(c => Convert.ToDouble(c.BenefitPoint)));
            }
        }
        public double F3C5
        {
            get
            {
                return Math.Min(F3C5MaxScore,
                ExtBenList.FindAll(c => c.BenefitType == "C5").Sum(c => Convert.ToDouble(c.BenefitPoint)));
            }
        }
        public double F3 { get { return F3C1 + F3C2 + F3C3 + F3C4 + F3C5; } }

        //F总分
        public double F1rate { get; set; }
        public double F2rate { get; set; }
        public double F3rate { get; set; }
        public double FSelf { get { return Math.Round(F1rate * F1Self + F2rate * F2 + F3rate * F3,2); } }
        public double FAudit { get { return F1rate * F1Audit + F2rate * F2 + F3rate * F3; } }

        //冗余数据，用于快速获取综测的总分的值
        public double ff1se { get; set; }
        public double ff1au { get; set; }
        public double ff2b1 { get; set; }
        public double ff2b2 { get; set; }
        public double ff3 { get; set; }
        public double ffself { get; set; }
        public double ffaudit { get; set; }

        

        //构造函数，包含个人信息
        public ComEvaluation(string UserId, string Grade)
        {
            ComEvaId = Guid.NewGuid().ToString();
            this.UserId = UserId;
            this.Grade = Grade;
            this.State = "未填报";
            Intialize();
        }

        public ComEvaluation()
        {
            ComEvaId = Guid.NewGuid().ToString();
            Intialize();
        }

        public void Intialize(double F3C1 = 10, double F3C2 = 10, double F3C3 = 10, double F3C4 = 10, double F3C5 = 10,
                              double F2B2rate = 0.002, double F1rate = 0.10, double F2rate = 0.7, double F3rate = 0.20,
                              int F1n = 5, int F2n2 = 8, int F3n = 5, string begin = "2022", string end = "2023")//初始化各项的数目
        {
            F1num = F1n;
            F2B2num = F2n2;
            F3num = F3n;
            F3C1MaxScore = F3C1;
            F3C2MaxScore = F3C2;
            F3C3MaxScore = F3C3;
            F3C4MaxScore = F3C4;
            F3C5MaxScore = F3C5;
            this.F1rate = F1rate;
            this.F2rate = F2rate;
            this.F3rate = F3rate;
            this.F2B2rate = F2B2rate;
            this.BeginTime = begin;
            this.EndTime = end;
            this.ff1au = 0;
            this.ff1se=0;
            this.ff2b1 = 0;
            this.ff2b2 = 0;
            this.ff3 = 0;
            this.ffself = 0;
            this.ffaudit = 0;
            A = new List<ComEvaAn>(F1n);
            B1 = new List<StudentCourseMark>();
            B2 = new List<StudentCourseMark>(F2n2);
            ExtBenList = new List<ExtraBenefit>();
        }

        //每次有变化时更新冗余数据
        public void update(int i)
        {
            switch (i)
            {
                case (0):
                    ffself = ffself - F1rate * ff1se + F1rate * F1Self;
                    ff1se = F1Self;
                    ffself = Math.Round(ffself, 3);
                    break;
                case (1):
                    ffaudit = ffaudit - F1rate * ff1au + F1rate * F1Audit;
                    ff1au = F1Audit;
                    ffaudit = Math.Round(ffaudit, 3);
                    break;
                case (2):
                    ffself = ffself - F2rate * (ff2b1 - F2B1);
                    ffaudit = ffaudit - F2rate * (ff2b1 - F2B1);
                    ff2b1 = F2B1;
                    ffself = Math.Round(ffself, 3);
                    ffaudit = Math.Round(ffaudit, 3);
                    break;
                case (3):
                    ffself = ffself - F2rate * F2B2rate*(ff2b2 - F2B2);
                    ffaudit = ffaudit - F2rate * F2B2rate * (ff2b2 - F2B2);
                    ff2b2 = F2B2;
                    ffself = Math.Round(ffself, 3);
                    ffaudit = Math.Round(ffaudit, 3);
                    break;
                case (4):
                    ffself = ffself - F3rate * (ff3 - F3);
                    ffaudit = ffaudit - F3rate * (ff3 - F3);
                    ff3 = F3;
                    ffself = Math.Round(ffself, 3);
                    ffaudit = Math.Round(ffaudit, 3);
                    break;
            }
        }
        //F1的填写及查看
        //学生填写自评
        public void WriteF1Self(int i, string aself = "0", string aremark = "")
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].ASelf = aself;
            A[i].ARemark = aremark;
            update(0);
        }

        //辅导员填写审核分
        public void WriteF1Audit(int i, string aaudit="0")
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].AAudit = aaudit;
            update(1);
        }

        //F2的填写和查看
        public void AddF2B1(List<StudentCourseMark> marks)
        {
            B1=marks;
            update(2);
        }

        public void AddF2B2(List<StudentCourseMark> marks)
        {
            //选择总分*绩点最高的放在前面
            marks.Sort((m, n) => Convert.ToInt16(m.stuMark * m.coursePoint - n.stuMark * n.coursePoint));
            //List<StudentCourseMark> marks1 = marks.Take(F2B2num).ToList<StudentCourseMark>();

            for(int i = 0; i < F2B2num; i++)
            {
                B2.Add (marks[i]);           
            }
            update(3);
        }

        //F3的填写和查看
        public void AddF3(List<ExtraBenefit> benefits)
        {
            if (benefits != null)
            {
                for(int i = 0; i < benefits.Count(); i++) { 
                ExtBenList.Add(benefits[i]);}
            }
            update(4);
        }
        

        //对综测表的操作
        public void submit()
        {
            State = "未审批";
        }
        //对综测表的审核成功操作
        public void auditsucc()
        {
            State = "已过审";
        }
        //对综测表的审核失败操作
        public void auditfail()
        {
            State = "未过审";
        }
    }
}