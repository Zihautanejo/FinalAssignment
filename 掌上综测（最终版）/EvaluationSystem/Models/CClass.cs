using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace EvaluationSystem.Models
{
    public class CClass
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CClassId { get; set; }
        public int ClassNum { get; set; }//班级号
        public string Grade { get; set; }//年级
        public string Academy { get; set; }//学院
        public string Major { get; set; }

       
        public double AvaragePoint { get; set; }//平均综测成绩
        public int NotSubmitNum
        { get; set; }//未提交人数

        public int PrecautionNum
        {
            get;set;
        }//学业预警人数

        public double AvgGPA
        {
            get;set;
        }//班级平均GPA

     
        public CClass()
        {

        }
        public CClass( int num, string cgrade, string cacademy)
        {
            
            this.ClassNum = num;
            this.Grade = cgrade;
            this.Academy = cacademy;
            this.NotSubmitNum = 0;
            this.PrecautionNum = 0;
            this.AvaragePoint = 0;
            this.AvgGPA = 0;
        }

    }
}