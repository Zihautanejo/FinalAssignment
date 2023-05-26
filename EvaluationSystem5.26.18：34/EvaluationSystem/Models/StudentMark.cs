using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    //学生成绩单类
    public class StudentMark
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //成绩单id
        [Key]
        public string StuMarkId { get; set; }
        //学号
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        //学期
        public string Semester { get; set; }
        public string Grade { get; set; }
        //成绩列表
        public List<StudentCourseMark> SCMark { get; set; } = new List<StudentCourseMark>();

        public StudentMark(string UserId, string Grade,string Semester,List<StudentCourseMark> SCMark)
        {
            this.UserId = UserId;
            this.Grade = Grade;
            this.Semester = Semester;
            this.SCMark = SCMark;
        }

    }
}

