using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace EvaluationSystem.Models
{
    public class CClass
    {
        [Key]
        public string ClassId { get; set; }
        public int ClassNum { get; set; }//班级号
        public string Grade { get; set; }//年级
        public string Academy { get; set; }//学院

        public double AvaragePoint { get { return this.avaragePoint; } set { this.avaragePoint = value; } }//平均综测成绩
        public int NotSubmitNum
        {
            get { return this.notSubmitNum; }
            set
            {
                if (value < 0) throw new ArgumentException("人数不能为负数！");
                else this.notSubmitNum = value;
            }
        }//未提交人数

        private int notSubmitNum;
        private double avaragePoint;
        public CClass()
        {

        }
        public CClass(string id, int num, string cgrade, string cacademy)
        {
            this.ClassId = id;
            this.ClassNum = num;
            this.Grade = cgrade;
            this.Academy = cacademy;
        }

    }
}