using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Models
{
    public class StudentInfo
    {
        public string StuId { get; set; }
        public string College { get; set; }
        public string Major { get; set; }
        public int Semester { get; set; }
        public string Grade { get; set; }
        public string ClassId { get; set; }

        public string TutorId { get; set; }

        public StudentInfo(string UserId, string College, string Major, int Semester, string grade,/*Transcript transcript,*/string ClassId, string TutorId)
        {
            this.StuId = UserId;
            this.College = College;
            this.Major = Major;
            this.Semester = Semester;
            Grade = grade;
            /*this.Transcript = transcript;*/
            this.ClassId = ClassId;
            this.TutorId = TutorId;
        }

       
        public StudentInfo() { }
    }

}
