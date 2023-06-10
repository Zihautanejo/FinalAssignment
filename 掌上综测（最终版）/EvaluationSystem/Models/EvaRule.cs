using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EvaluationSystem.Models
{
    public class EvaRule
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvaRuleId { get; set; }

        //年级
        public string Grade { get; set; }

        //学院
        public string  Academy { get; set; }

        public string C1 { get; set; }
        public string C2 { get; set; }

        public string C3 { get; set; }

        public string C4 { get; set; }

        public string  C5 { get; set; }

        public EvaRule() { }
        public EvaRule(string grade,string academy,string c1, string c2,string c3,string c4,string c5)
        {
            this.Grade = grade;
            this.Academy = academy;
            this.C1 = c1;
            this.C2 = c2;
            this.C3 = c3;
            this.C4 = c4;
            this.C5 = c5;
        }

    }
}
