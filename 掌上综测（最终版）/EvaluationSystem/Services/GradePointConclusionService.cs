using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.ComponentModel;
//using OfficeOpenXml;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;

namespace EvaluationSystem.Services
{
    public interface IGradePointConclusionService
    {

        GradePointConclusion GetStudentGradePointConclusion(string userId, string EnrollmentYear, string major);
        List<ClassGradeDetail> getStuGradesConcluInClass(string grade, string major, string? classnum);
        List<ClassGradeDetail> sortClassGradeDetailByUserId(string grade, string major, string classnum);
        List<ClassGradeDetail> sortClassGradeDetailByGpaDesc(string grade, string major, string classnum);
        List<ClassGradeDetail> sortClassGradeDetailByGpa(string grade, string major, string classnum);
        List<ClassGradeDetail> query(string grade, string major, string? classnum, int rank, int status);
        int modifyPrecautionNum(string grade, string major, string classnum);
        List<int> modifyall(string major, string grade, int? num);
        public List<CClass> getClassGradeConclusion(string grade, string major);

    }
    public class GradePointConclusionService : IGradePointConclusionService
    {
        private readonly IStudentCourseMarkService _scmarkDao;
        EvaDbContext _dbContext;
        //private readonly UserService _userDao = new UserService(_dbContext);

        public GradePointConclusionService(EvaDbContext dbContext)
        {
            _dbContext = dbContext;
            classDao = new ClassService(dbContext);
        }

        ClassService classDao;
        public GradePointConclusion GetStudentGradePointConclusion(string userId, string EnrollmentYear, string major)
        {
            List<StudentCourseMark> stumarks = _dbContext.StudentCourseMark.Where(x => x.UserId == userId).ToList();

            GradePointConclusion gradeconclusion = new GradePointConclusion(userId, EnrollmentYear, major, stumarks);

            return gradeconclusion;
        }
        //获取班级学生成绩
        public List<ClassGradeDetail> getStuGradesConcluInClass(string grade, string major, string? classnum)
        {
            int classid;
            List<ClassGradeDetail> gradePointConclusions = new List<ClassGradeDetail>();
            List<StudentInfo> stuinfo = new List<StudentInfo>();
            if (classnum != null)
            {
                classid = int.Parse(classnum);
                stuinfo = _dbContext.Students.Where(x => x.ClassId == classid && x.EnrollmentYear == grade && x.Major == major).ToList();
            }
            else
            {
                stuinfo = _dbContext.Students.Where(x => x.EnrollmentYear == grade && x.Major == major).ToList();
            }

            foreach (StudentInfo stu in stuinfo)
            {
                List<StudentCourseMark> stumarks = _dbContext.StudentCourseMark.Where(x => x.UserId == stu.UserId).ToList();
                if (stumarks != null)
                {
                    GradePointConclusion gradeconclusion = new GradePointConclusion(stu.UserId, grade, major, stumarks);
                    User u = _dbContext.Users.FirstOrDefault(x => x.UserId == stu.UserId);
                    if (u != null)
                    {
                        ClassGradeDetail classgradedetail = new ClassGradeDetail(stu.UserId, u.Name, gradeconclusion.GPA);
                        gradePointConclusions.Add(classgradedetail);
                    }

                }

            }

            return gradePointConclusions;
        }

        //对上面获取到的List进行排序
        //按学号排序
        public List<ClassGradeDetail> sortClassGradeDetailByUserId(string grade, string major, string classnum)
        {
            List<ClassGradeDetail> list = getStuGradesConcluInClass(grade, major, classnum);
            list = list.OrderBy(x => x.UserId).ToList();
            return list;
        }

        //按平均绩点降序
        public List<ClassGradeDetail> sortClassGradeDetailByGpaDesc(string grade, string major, string classnum)
        {
            List<ClassGradeDetail> list = getStuGradesConcluInClass(grade, major, classnum);
            list = list.OrderByDescending(x => x.GPA).ToList();
            return list;
        }

        //按平均绩点升序
        public List<ClassGradeDetail> sortClassGradeDetailByGpa(string grade, string major, string classnum)
        {
            List<ClassGradeDetail> list = getStuGradesConcluInClass(grade, major, classnum);
            list = list.OrderBy(x => x.GPA).ToList();
            return list;
        }

        //查询学业预警状态
        public List<ClassGradeDetail> query(string grade, string major, string? classnum, int rank, int status)
        {

            List<ClassGradeDetail> list = getStuGradesConcluInClass(grade, major, classnum);
            if (status == 1)
            {
                list = list.Where(x => x.GPA >= 3).ToList();
            }
            if (status == 2)
            {
                list = list.Where(x => x.GPA < 3).ToList();
            }
            switch (rank)
            {
                case 0:
                    list = list.OrderBy(x => x.UserId).ToList();
                    break;
                case 1:
                    list = list.OrderByDescending(x => x.GPA).ToList();
                    break;
                case 2:
                    list = list.OrderBy(x => x.GPA).ToList();
                    break;
            }


            return list;
        }

        //统计并修改班级学业预警人数
        public int modifyPrecautionNum(string grade, string major, string classnum)
        {
            int cclass = int.Parse(classnum);
            int num;
            List<ClassGradeDetail> list = query(grade, major, classnum, 0, 2);
            num = list.Count();
            classDao.ModifyPrecautionNum(major, grade, cclass, num);
            return num;
        }

        public List<int> modifyall(string major, string grade, int? num)
        {
            List<CClass> list = classDao.GetCClassesByMajorGrade(major, grade, num);
            List<int> i = new List<int>();
            foreach (CClass c in list)
            {
                Console.WriteLine(c.ClassNum);
                i.Add(modifyPrecautionNum(c.Grade, c.Major, c.ClassNum.ToString()));

            }
            return i;
        }

        //分析班级学生成绩
        public List<CClass> getClassGradeConclusion(string grade, string major)
        {
            List<CClass> classlist = classDao.GetCClassesByMajorGrade(major, grade, null);
            int classnum = classlist.Count();
            Console.WriteLine("ttttttttttttttttttt" + classnum);
            for (int num = 1; num <= classnum; num++)
            {
                List<ClassGradeDetail> classgrade = getStuGradesConcluInClass(grade, major, num.ToString());
                int pnum = 0;//班级人数
                double totalGPA = 0;
                double avgGPA = 0;
                int precautionnum = 0;//预警人数
                if (classgrade.Count != 0)
                {
                    foreach (ClassGradeDetail c in classgrade)
                    {
                        pnum++;
                        totalGPA += c.GPA;
                        if (c.GPA < 3)
                        {
                            precautionnum++;
                        }


                    }
                    avgGPA = Math.Round(totalGPA / pnum, 2);
                    classDao.ModifyPrecautionNum(grade, major, num, precautionnum);
                    classDao.ModifyAvgGpa(grade, major, num, avgGPA);
                    //ClassGradeConclusion classconcluItem = new CClass(major, num.ToString(), avgGPA, precautionnum);
                    //classconclu.Add(classconcluItem);
                }

            }
            List<CClass> classconclu = classDao.GetCClassesByMajorGrade(major, grade, null);
            return classconclu;
        }
    }

}
