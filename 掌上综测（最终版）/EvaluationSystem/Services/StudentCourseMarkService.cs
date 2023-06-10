using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.Data;
using ExcelDataReader;

namespace EvaluationSystem.Services
{
    public interface IStudentCourseMarkService
    {
        // void ImportStudentCourseMark(IFormFile file);
        List<StudentCourseMark> GetStudentCourseMark(string userId, string grade, string semester, string classType, string? input);
        List<StudentCourseMark> QueryStudentCourseMarkById(int Id);
        void SaveStudentCourseMark(List<StudentCourseMark> scmark);

        List<StudentCourseMark> ImportExcelData(IFormFile file);

    }
    public class StudentCourseMarkService : IStudentCourseMarkService
    {
        private readonly IStudentCourseMarkService _scmarkDao;
        EvaDbContext _dbContext;

        public StudentCourseMarkService(EvaDbContext dbContext)
        {
            _dbContext = dbContext;
            comDao = new ComEvaService(dbContext);
            userDao = new UserService(dbContext);
        }
        ComEvaService comDao;
        UserService userDao;
        //导入成绩
        public List<StudentCourseMark> ImportExcelData(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var dataTable = result.Tables[0];

                    List<StudentCourseMark> marks = new List<StudentCourseMark>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var courseName = row["courseName"].ToString();
                        var courseType = row["courseType"].ToString();
                        var coursePoint = Convert.ToDouble(row["coursePoint"]);
                        var courseId = row["courseId"].ToString();
                        var userId = row["UserId"].ToString();
                        var stuMark = Convert.ToInt32(row["stuMark"]);
                        var grade = row["grade"].ToString();
                        var semester = row["semester"].ToString();
                        var isFailed = Convert.ToBoolean(row["isFailed"]);
                        var isRetook = Convert.ToBoolean(row["isRetook"]);
                        var stuGradePoint = Convert.ToDouble(row["stuGradePoint"]);

                        string stugrade = userDao.QueryStuInfo(userId).Grade;
                        ComEvaluation com = comDao.QueryComEvaByStudent(userId, stugrade)[0];


                        StudentCourseMark mark = new StudentCourseMark(courseName, courseType, coursePoint, courseId, userId, stuMark, grade, semester, isFailed, isRetook, stuGradePoint);

                        // 设置导航属性
                        mark.Comeva1 = com;
                        mark.Comeva2 = com;

                        marks.Add(mark);
                        
                        

                    }

                    _dbContext.StudentCourseMark.AddRange(marks);
                    _dbContext.SaveChanges();
                    return marks;
                }

            }
        }

        public List<StudentCourseMark> GetStudentCourseMark(string userId, string grade, string semester, string classType, string? input)
        {
            List<StudentCourseMark> stum = new List<StudentCourseMark>();
            if (grade == "all" && semester == "all" && classType == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId).ToList();

            }
            else if (grade == "all" && classType == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.semester == semester).ToList();
            }
            else if (semester == "all" && classType == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.grade == grade).ToList();
            }
            else if (grade == "all" && semester == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.courseType == classType).ToList();
            }
            else if (grade == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.semester == semester && x.courseType == classType).ToList();
            }
            else if (semester == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.grade == grade && x.courseType == classType).ToList();
            }
            else if (classType == "all")
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.grade == grade && x.semester == semester).ToList();
            }
            else
            {
                stum = _dbContext.StudentCourseMark.Where(x => x.UserId == userId && x.grade == grade && x.semester == semester && x.courseType == classType).ToList();
            }

            if (input != null)
            {
                stum = stum.Where(x => x.courseName.Contains(input)).ToList();
            }

            return stum;
        }

        public List<StudentCourseMark> QueryStudentCourseMarkById(int Id)
        {
            var scmark = _dbContext.StudentCourseMark.Where(x => x.Id == Id);
            return scmark.ToList();
        }

        public void SaveStudentCourseMark(List<StudentCourseMark> scmark)
        {
            // 保存成绩到数据库
            _dbContext.StudentCourseMark.AddRange(scmark);
            _dbContext.SaveChanges();
        }
    }

}
