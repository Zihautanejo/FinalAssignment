using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    //学生每项课程成绩
    public class StudentCourseMark
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //自增序列号
        public int Id { get; set; }
        //课程名称
        public string courseName { get; set; }
        //课程类型 公共基础 专业必修 ……
        public string courseType { get; set; }
        //课程学分
        public double coursePoint { get; set; }
        //课程号
        public string courseId { get; set; }
        //学号
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        //学生成绩
        public int stuMark { get; set; }
        //修读学期
        public string grade { get; set; }
        public string semester { get; set; }
        //是否挂科
        public bool isFailed { get; set; }
        //是否重修
        public bool isRetook { get; set; }
        //本课程绩点
        public double stuGradePoint { get; set; }

        //导航属性
        //[ForeignKey("ComEvaId")]
        //public ComEvaluation? comeva1 = new ComEvaluation();

        //[ForeignKey("ComEvaId")]
        //public ComEvaluation? comeva2 = new ComEvaluation();


        [ForeignKey("ComEvaId")]
        public string Comeva1Id { get; set; }

        [ForeignKey("ComEvaId")]
        public string Comeva2Id { get; set; }

        [InverseProperty("B1")]
        public virtual ComEvaluation Comeva1 { get; set; }

        [InverseProperty("B2")]
        public virtual ComEvaluation Comeva2 { get; set; }



        public StudentCourseMark(string courseName, string courseType, double coursePoint, string courseId,
            string UserId, int stuMark, string grade,string semester, bool isFailed, bool isRetook, double stuGradePoint)
        {
            this.courseName = courseName;
            this.courseType = courseType;
            this.coursePoint = coursePoint;
            this.courseId = courseId;
            this.UserId = UserId;
            this.stuMark = stuMark;
            this.grade = grade;
            this.semester = semester;
            this.isFailed = isFailed;
            this.isRetook = isRetook;
            this.stuGradePoint = stuGradePoint;
        }

    }
}