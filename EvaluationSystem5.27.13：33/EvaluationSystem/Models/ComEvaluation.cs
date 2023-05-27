using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Mozilla;

namespace EvaluationSystem.Models
{
    public class ComEvaluation
    {
        [Key]
        //综测表的用户信息
        public string ComEvaId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string ClassId { get; set; }
        public string Grade { get; set; }
        public string Semester { get; set; }
        public int State { get; set; }//0为填写未提交，1为提交未审批，2为审批通过，3为审批未通过

        //综测表的基本信息
        //综测表的证明材料时间范围
        public string BeginTime { get; set; }
        public string EndTime { get; set; }

        //F1项--------------------------------------------------------
        //F1num代表F1细分的数目，F1MaxScore代表F1分数中取值的上限                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        public int F1num { get; set; }
        public double F1MaxScore { get => 100 / (F1num != 0 ? F1num : 1); }
        public List<ComEvaAn> A = new List<ComEvaAn>();
        //F1总分只能读不能写
        public double F1Self { get { return A.Sum(a => a.ASelf); } }
        public double F1Audit { get { return A.Sum(a => a.AAudit); } }

        //F2项-------------------------------------------------------
        public int F2B2num { get; set; }
        public double F2B2rate { get; set; }
        public List<StudentCourseMark> B1 = new List<StudentCourseMark>();
        //public List<Bn> FailedB1 = B1.FindAll(b => b.BState = false);
        public List<StudentCourseMark> B2 = new List<StudentCourseMark>();
        public double F2B1 { get { return (B1.Sum(b => b.stuMark * b.coursePoint)) / (B1.Sum(b => b.coursePoint)); } }
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
        public double FSelf { get { return F1rate * F1Self + F2rate * F2 + F3rate * F3; } }
        public double FAudit { get { return F1rate * F1Audit + F2rate * F2 + F3rate * F3; } }

        //构造函数，包含个人信息
        public ComEvaluation(string UserId, string Grade, string Semester, string ClassId)
        {
            ComEvaId = Guid.NewGuid().ToString();
            this.UserId = UserId;
            this.Grade = Grade;
            this.Semester = Semester;
            this.ClassId = ClassId;
            this.State = 0;
        }
        public ComEvaluation()
        {
            ComEvaId = Guid.NewGuid().ToString();
        }
        public void Intialize(double F3C1, double F3C2, double F3C3, double F3C4, double F3C5,
                              double F2B2rate, double F1rate, double F2rate, double F3rate,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)//初始化各项的数目
        {
            F1num = F1n;
            A = new List<ComEvaAn>(F1n);
            B1 = new List<StudentCourseMark>();
            F2B2num = F2n2;
            B2 = new List<StudentCourseMark>(F2n2);
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

        }
        //F1的填写及查看
        //学生填写自评
        public void WriteF1Self(int i, double aself = 0, string aremark = null)
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].ASelf = aself;
            A[i].ARemark = aremark;
        }

        //辅导员填写审核分
        public void WriteF1Audit(int i, double aaudit)
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            A[i].AAudit = aaudit;
        }
        //查询单个分数项
        public ComEvaAn QueryF1(int i)
        {
            if (i > F1num && i < 0) { throw new ApplicationException($"错误信息：F1中A{i}不在范围之内"); }
            return A[i];
        }

        //F2的填写和查看
        public void AddB2F1(List<StudentCourseMark> marks)
        {
            B1 = marks;
        }
        public void AddB2F2(List<StudentCourseMark> marks)
        {
            if (marks.Count() <= F2B2num)
            {
                B2 = marks;
            }
            else
            {
                //选择总分*绩点最高的放在前面
                marks.Sort((m, n) => Convert.ToInt16(m.stuMark * m.coursePoint - n.stuMark * n.coursePoint));
                List<StudentCourseMark> marks1 = marks.Take(F2B2num).ToList<StudentCourseMark>();
                B2 = marks;
            }

        }


        //F3的填写和查看
        public void AddF3(List<ExtraBenefit> benefits)
        {
            ExtBenList = benefits;
        }

        //对综测表的操作
        public void submit()
        {
            State = 1;
        }
        //对综测表的审核成功操作
        public void auditsucc()
        {
            State = 2;
        }
        //对综测表的审核失败操作
        public void auditfail()
        {
            State = 3;
        }
    }
}
