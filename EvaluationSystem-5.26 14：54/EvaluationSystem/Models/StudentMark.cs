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
        public string StuMarkId { get; set; }
        //学号
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        //学期
        public string Semester { get; set; }
        //成绩列表
        public List<StudentCourseMark> SCMark { get; set; } = new List<StudentCourseMark>();

        public StudentMark(string UserId, string Semester, string StuMarkId, List<StudentCourseMark> SCMark)
        {
            this.UserId = UserId;
            this.Semester = Semester;
            this.StuMarkId = StuMarkId;
            this.SCMark = SCMark;
        }

    }
}

