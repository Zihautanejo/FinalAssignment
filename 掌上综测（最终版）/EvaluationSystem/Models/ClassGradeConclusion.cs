namespace EvaluationSystem.Models
{
    public class ClassGradeConclusion
    {
        public string major { get; set; }
        public string classnum { get; set; }
        public double avgGPA { get; set; }
        public int precautionnum { get; set; }

        public ClassGradeConclusion(string major, string classnum, double avgGPA, int precautionnum)
        {
            this.major = major;
            this.classnum = classnum;
            this.avgGPA = avgGPA;
            this.precautionnum = precautionnum;
        }
    }
}
