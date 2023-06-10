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

        public string strongSubjects  { get; set; }
        public string weakSubjects { get; set; }
        public string failSubjects { get; set; }
        public GradePointConclusion(string userId,string EnrollmentYear, string major, List<StudentCourseMark> stumarks)
        {
            //要通过全部成绩单和该学生所在的专业以及年级来更新综合成绩分析
            //好像漏了跨学院的课
            double totalgradepoint = 0;
            double totalmajorcompulsorygradepoint = 0;
            double totalmajorselectiveegradepoint = 0;
            UserId=userId;

            //判断是否有优势科目弱势科目以及挂科科目
            int flag1 = 0;
            int flag2 = 0;
            int flag3 = 0;
            int cnt1 = 0;
            int cnt2 = 0;
            int cnt3 = 0;

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
                
                    if (stumarks[i].isFailed == false)
                    {
                        totalTakePoint += stumarks[i].coursePoint;
                        totalgradepoint += stumarks[i].coursePoint * stumarks[i].stuGradePoint;
                        if (stumarks[i].stuMark >= 95)
                        {
                            if (cnt1 != 0) strongSubjects += "、";
                            strongSubjects +=stumarks[i].courseName;
                            flag1 = 1;
                            cnt1++;
                        }
                        if(stumarks[i].stuMark <=75)
                        {
                            if (cnt2 != 0) weakSubjects += "、";
                            weakSubjects += stumarks[i].courseName;
                            flag2 = 1;
                            cnt2++;
                        }
                        switch (stumarks[i].courseType)
                        {
                            case "公共基础必修课":
                                basicCourseTakePoint += stumarks[i].coursePoint;
                                break;
                            case "专业必修课":
                                majorCompulsoryTakePoint += stumarks[i].coursePoint;
                                totalmajorcompulsorygradepoint += stumarks[i].coursePoint * stumarks[i].stuGradePoint;
                            break;
                            case "专业选修课":
                                majorSelectiveTakePoint += stumarks[i].coursePoint;
                                totalmajorselectiveegradepoint += stumarks[i].coursePoint * stumarks[i].stuGradePoint;
                            break;
                            case "艺术体验与审美鉴赏":
                                generalArtFieldTakePoint += stumarks[i].coursePoint;
                                generalCourseTakePoint += stumarks[i].coursePoint;
                                break;
                            case "中华文化与世界文明":
                                generalCurtureFieldTakePoint += stumarks[i].coursePoint;
                                generalCourseTakePoint += stumarks[i].coursePoint;
                                break;
                            case "社会科学与当代社会":
                                generalSocietyFieldTakePoint += stumarks[i].coursePoint;
                                generalCourseTakePoint += stumarks[i].coursePoint;
                                break;
                            case "科学精神与生命关怀":
                                generalScienceFieldTakePoint += stumarks[i].coursePoint;
                                generalCourseTakePoint += stumarks[i].coursePoint;
                                break;
                            default:
                                break;
                        }
                }
                else
                {
                    if (cnt3 != 0) failSubjects += "、";
                    failSubjects +=stumarks[i].courseName;
                    flag3 = 1;
                    cnt3++;
                }
                    

            }
            if(totalTakePoint != 0)
            {
                GPA = Math.Round(totalgradepoint / totalTakePoint, 2);
            }
            if (majorCompulsoryTakePoint != 0)
            {
                majorCompulsoryGPA = Math.Round(totalmajorcompulsorygradepoint / majorCompulsoryTakePoint, 2);
            }
            if (majorSelectiveTakePoint != 0)
            {
                majorSelectiveGPA = Math.Round(totalmajorselectiveegradepoint / majorSelectiveTakePoint, 2);
            }
            

            if(flag1 == 0) {
                strongSubjects="暂无";
            }
            if (flag2 == 0)
            {
                weakSubjects = "暂无";
            }
            if (flag3 == 0)
            {
                failSubjects = "暂无";
            }
        }

    }
}