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
        //课程类型 { 公共基础必修课, 专业必修课, 专业选修课, 艺术体验与审美鉴赏, 中华文化与世界文明, 社会科学与当代社会, 科学精神与生命关怀 }
        public string courseType;
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
        public double stuGradePoint { get; set; }

        public StudentCourseMark(string courseName, string courseType, double coursePoint, string courseId,
            string UserId, int stuMark, string courseTakeSemester,bool isRetook)
        {
            this.courseName = courseName;
            this.courseType = courseType;
            this.coursePoint = coursePoint;
            this.courseId = courseId;
            this.UserId = UserId;
            this.stuMark = stuMark;
            this.courseTakeSemester = courseTakeSemester;

            if(stuMark >= 60)
            {
                isFailed = false;
            }
            else
            {
                isFailed= true;
            }
            this.isRetook = isRetook;

            if(isFailed)
            {
                stuGradePoint = 0;
            }
            else
            {
                if (stuMark >= 90)
                {
                    stuGradePoint = 4.0;
                }
                else if (stuMark >= 85)
                {
                    stuGradePoint = 3.7;
                }
                else if (stuMark >= 82)
                {
                    stuGradePoint = 3.3;
                }
                else if (stuMark >= 78)
                {
                    stuGradePoint = 3.0;
                }
                else if (stuMark >= 75)
                {
                    stuGradePoint = 2.7;
                }
                else if (stuMark >= 72)
                {
                    stuGradePoint = 2.3;
                }
                else if (stuMark >= 68)
                {
                    stuGradePoint = 2.0;
                }
                else if (stuMark >= 64)
                {
                    stuGradePoint = 1.5;
                }
                else if (stuMark >= 60)
                {
                    stuGradePoint = 1.0;
                }
            }

        }

    }
}