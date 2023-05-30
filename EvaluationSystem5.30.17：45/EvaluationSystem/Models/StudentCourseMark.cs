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
        public string courseTakeSemester { get; set; }
        //是否挂科
        public bool isFailed { get; set; }
        //是否重修
        public bool isRetook { get; set; }
        //本课程绩点
        public int stuGradePoint { get; set; }

        public StudentCourseMark(string courseName, string courseType, double coursePoint, string courseId,
            string UserId, int stuMark, string courseTakeSemester, bool isFailed, bool isRetook, int stuGradePoint)
        {
            this.courseName = courseName;
            this.courseType = courseType;
            this.coursePoint = coursePoint;
            this.courseId = courseId;
            this.UserId = UserId;
            this.stuMark = stuMark;
            this.courseTakeSemester = courseTakeSemester;
            this.isFailed = isFailed;
            this.isRetook = isRetook;
            this.stuGradePoint = stuGradePoint;
        }

    }
}