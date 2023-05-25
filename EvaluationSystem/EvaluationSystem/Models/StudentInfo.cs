
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class StudentInfo
    {
        [Key]
        //学号
        public string UserId { get; set; }
        //学院
        public string Academy { get; set; }
        //专业
        public string Major { get; set; }
        //当前学期
        public string Semester { get; set; }
        //年级
        public string Grade { get; set; }
        //班级
        public string ClassId { get; set; }
        //辅导员
        public string TutorId { get; set; }
        //入学年份
        public string EnrollmentYear { set; get; }
       

        public StudentInfo(string UserId, string academy, string Major, string Semester, string grade,/*Transcript transcript,*/string ClassId, string TutorId,string enrollmentyear)
        {
            this.UserId = UserId;
            this.Academy =academy;
            this.Major = Major;
            this.Semester = Semester;
            this.Grade = grade;
            /*this.Transcript = transcript;*/
            this.ClassId = ClassId;
            this.TutorId = TutorId;
            this.EnrollmentYear = enrollmentyear;
        }

       
        public StudentInfo() { }
    }

}
