using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSystem.Models
{
    public class GradePointConclusion
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //自增序列号
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public double totalNeedPoint { get; set; }
        public double totalTakePoint { get; set; }
        public double majorCompulsoryNeedPoint { get; set; }
        public double majorCompulsoryTakePoint { get; set; }
        public double majorSelectiveNeedPoint { get; set; }
        public double majorSelectiveTakePoint { get; set; }
        public double basicCourseNeedPoint { get; set; }
        public double basicCourseTakePoint { get; set; }
        public double generalCourseNeedPoint { get; set; }
        public double generalCourseTakePoint { get; set; }
        public double generalArtFieldTakePoint { get; set; }
        public double generalCurtureFieldTakePoint { get; set; }
        public double generalSocietyFieldTakePoint { get; set; }
        public double generalScienceFieldTakePoint { get; set; }
        public double GPA { get; set; }
        public double majorCompulsoryGPA { get; set; }
        public double majorSelectiveGPA { get; set; }

        public GradePointConclusion(string EnrollmentYear, string major, List<StudentMark> stumarks)
        {
            //要通过全部成绩单和该学生所在的专业以及年级来更新综合成绩分析
            //好像漏了跨学院的课
            if (EnrollmentYear == "2021" || EnrollmentYear == "2020" || EnrollmentYear == "2019" || EnrollmentYear == "2018")
            {
                switch (major)
                {
                    case "计算机科学与技术":
                        totalNeedPoint = 140;
                        basicCourseNeedPoint = 50;
                        generalCourseNeedPoint = 12;
                        majorCompulsoryNeedPoint = 35;
                        majorSelectiveNeedPoint = 43;
                        break;
                    case "计算机科学与技术卓越工程班":
                        totalNeedPoint = 140;
                        basicCourseNeedPoint = 50;
                        generalCourseNeedPoint = 12;
                        majorCompulsoryNeedPoint = 38;
                        majorSelectiveNeedPoint = 40;
                        break;
                    case "软件工程":
                        totalNeedPoint = 140;
                        basicCourseNeedPoint = 50;
                        generalCourseNeedPoint = 12;
                        majorCompulsoryNeedPoint = 34;
                        majorSelectiveNeedPoint = 44;
                        break;
                    case "软件工程卓越工程班":
                        totalNeedPoint = 140;
                        basicCourseNeedPoint = 50;
                        generalCourseNeedPoint = 12;
                        majorCompulsoryNeedPoint = 34;
                        majorSelectiveNeedPoint = 44;
                        break;
                    case "人工智能":
                        totalNeedPoint = 140;
                        basicCourseNeedPoint = 50;
                        generalCourseNeedPoint = 12;
                        majorCompulsoryNeedPoint = 32;
                        majorSelectiveNeedPoint = 46;
                        break;
                }
            }
            for (int i = 0; i < stumarks.Count; i++)
            {
                for (int j = 0; j < stumarks[i].SCMark.Count; j++)
                {
                    if (stumarks[i].SCMark[j].isFailed == false)
                    {
                        totalTakePoint += stumarks[i].SCMark[j].coursePoint;

                        switch (stumarks[i].SCMark[j].courseType)
                        {
                            case "公共基础必修课":
                                basicCourseTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "专业必修课":
                                majorCompulsoryTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "专业选修课":
                                majorSelectiveTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "艺术体验与审美鉴赏":
                                generalArtFieldTakePoint += stumarks[i].SCMark[j].coursePoint;
                                generalCourseTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "中华文化与世界文明":
                                generalCurtureFieldTakePoint += stumarks[i].SCMark[j].coursePoint;
                                generalCourseTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "社会科学与当代社会":
                                generalSocietyFieldTakePoint += stumarks[i].SCMark[j].coursePoint;
                                generalCourseTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            case "科学精神与生命关怀":
                                generalScienceFieldTakePoint += stumarks[i].SCMark[j].coursePoint;
                                generalCourseTakePoint += stumarks[i].SCMark[j].coursePoint;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }

    }
}
