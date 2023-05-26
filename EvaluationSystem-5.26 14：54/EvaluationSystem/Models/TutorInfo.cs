using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class TutorInfo
    {
        [Key]
        public string UserId { get; set; }
        //所在学校
        public string College { get; set; }
        //所属系
        public string Major { get; set; }
        //所处学期
        public int Semester { get; set; }
        //所处年级
        public string Grade { get; set; }
        //所带班级
        //public List<Class> classes = new List<Class>();
        public TutorInfo(string userId, string College, string Major, int Semester, string grade/*,List<CLass> classes*/)
        {
            this.UserId = userId;
            this.College = College;
            this.Major = Major;
            this.Semester = Semester;
            this.Grade = grade;
            // this.classes=classes;
        }
       
        public TutorInfo() { }
    }
}
