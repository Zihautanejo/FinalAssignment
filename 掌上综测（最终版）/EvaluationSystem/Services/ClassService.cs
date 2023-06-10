using System.Diagnostics;
using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EvaluationSystem.Services
{
    public class ClassService
    {
        EvaDbContext dbContext;
        public ClassService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //增加班级
        public void AddClass(CClass cclass)
        {
            dbContext.CClass.Add(cclass);
            dbContext.SaveChanges();
        }

        //删除班级
        public void RemoveClass(CClass cclass)
        {
            dbContext.CClass.Remove(cclass);
            dbContext.SaveChanges();
        }

        //修改班级信息
        public void ModifyClass(CClass cclass)
        {
            RemoveClass(cclass);
            AddClass(cclass);
        }
        //修改班级平均分
        public void ModifyAvaPoint(int classid, double point)
        {
            CClass cclass = GetClass(classid);
            cclass.AvaragePoint = point;
            ModifyClass(cclass);

        }
        //修改班级未提交综测人数
        public void ModifyNotSubNum(int classid, int num)
        {
            CClass cclass = GetClass(classid);
            cclass.NotSubmitNum = num;
            ModifyClass(cclass);
        }

        //修改班级学业预警人数
        public void ModifyPrecautionNum(string major, string grade, int classnum, int modifynum)
        {
            CClass cclass = GetCClassesByMajorGradeNum(grade, major, classnum);
            if (cclass != null)
            {
                cclass.PrecautionNum = modifynum;
                ModifyClass(cclass);
            }

        }

        //修改班级平均GPA
        public void ModifyAvgGpa(string major, string grade, int classnum, double gpa)
        {
            CClass cclass = GetCClassesByMajorGradeNum(grade, major, classnum);
            if (cclass != null)
            {
                cclass.AvgGPA = gpa;
                ModifyClass(cclass);
            }

        }

        //查询班级
        public CClass GetClass(int id)
        {
            var query = dbContext.CClass.FirstOrDefault(c => c.CClassId == id);
            if (query == null)
            {
                throw new ArgumentException("不存在该班级！");
            }
            return query;
        }


        //根据专业年级查找班级
        public List<CClass> GetCClassesByMajorGrade(string major, string grade, int? num)
        {
            if (num == null)
            {
                if (major == "all")
                {
                    if (grade == "all")
                    {
                        return dbContext.CClass.ToList();
                    }
                    else
                    {
                        var query = dbContext.CClass
                        .Where(c => c.Grade == grade);
                        return query.ToList();
                    }

                }
                else
                {
                    if (grade == "all")
                    {
                        return dbContext.CClass
                            .Where(c => c.Major == major)
                            .ToList();
                    }
                    else
                    {
                        var query = dbContext.CClass
                       .Where(c => c.Major == major)
                       .Where(c => c.Grade == grade);
                        return query.ToList();
                    }

                }
            }
            else
            {
                var query = dbContext.CClass
              .Where(c => c.Major == major)
              .Where(c => c.Grade == grade)
              .Where(c => c.ClassNum == num);
                return query.ToList();
            }
        }

        public CClass GetCClassesByMajorGradeNum(string major, string grade, int num)
        {
            var query = dbContext.CClass.SingleOrDefault
                (c => c.Major == major && c.Grade == grade && c.ClassNum == num);
            if(query==null)
            {
                throw new ArgumentException("不存在符合条件班级！");
            }
            return query;
        }

        //根据班级ID查询班级信息 
        public double GetClassAvaScore(int id)
        {
            return GetClass(id).AvaragePoint;
        }



        public int GetClassNotSubNum(int id)
        {
            return GetClass(id).NotSubmitNum;
        }



        //排序
        //按班号排序
        public List<CClass> SortByClassNum(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.ClassNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderBy(c => c.ClassNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.ClassNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
                .OrderBy(c => c.ClassNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }

        }

        //按平均综测成绩升序排序
        public List<CClass> SortByAvaPoint(string major, string grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.AvaragePoint);
                return query==null?throw new ArgumentException("不存在相应班级！"):query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderBy(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }

        }
        //按平均综测成绩降序排序
        public List<CClass> SortByAvaPointDescend(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderByDescending(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.AvaragePoint);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }



        }

        //按未提交人数升序排序
        public List<CClass> SortByNotSubNum(string? academy, string? grade)
        {
            if (academy != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Academy == academy)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (academy != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Academy == academy)
               .OrderBy(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
        }

        //按未提交人数降序排序
        public List<CClass> SortByNotSubNumDescend(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderByDescending(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.NotSubmitNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }



        }

        //按学业预警人数升序排序
        public List<CClass> SortByPrecautionNum(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Academy == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Academy == major)
               .OrderBy(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
        }

        //按学业预警人数降序排序
        public List<CClass> SortByPrecautionNumDescend(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderByDescending(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.PrecautionNum);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }



        }
        //按GPA升序排序
        public List<CClass> SortByAvgGpa(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderBy(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
        }

        //按GPA降序排序
        public List<CClass> SortByAvgGpaDescend(string? major, string? grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderByDescending(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.AvgGPA);
                return query == null ? throw new ArgumentException("不存在相应班级！") : query.ToList();
            }



        }
        //根据班级id 年级 专业 获取学业预警人数
        public int QueryPrecautionNum(int id, string major, string grade)
        {
            var query = dbContext.CClass.FirstOrDefault(c => c.ClassNum == id && c.Major == major && c.Grade == grade);
            if (query == null)
            {
                throw new ArgumentException("不存在该数据！");
            }
            return query.PrecautionNum;


        }


    }
}