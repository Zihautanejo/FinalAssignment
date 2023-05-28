using System.Diagnostics;
using EvaluationSystem.DAO;
using EvaluationSystem.Models;

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
        public void ModifyAvaPoint(string classid, double point)
        {
            CClass cclass = GetClass(classid);
            cclass.AvaragePoint = point;
            ModifyClass(cclass);

        }
        //修改班级未提交综测人数
        public void ModifyNotSubNum(string classid, int num)
        {
            CClass cclass = GetClass(classid);
            cclass.NotSubmitNum = num;
            ModifyClass(cclass);
        }


        //查询班级
        public CClass GetClass(string id)
        {
            var query = dbContext.CClass.FirstOrDefault(c => c.ClassId == id);
            if (query == null)
            {
                throw new ArgumentException("不存在该班级！");
            }
            return query;
        }


        //根据学院年级班级号查找班级
        public List<CClass> GetCClasses(string academy, string grade, int? num)
        {
            if (num == null)
            {
                var query = dbContext.CClass
               .Where(c => c.Academy == academy)
               .Where(c => c.Grade == grade);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .Where(c => c.Academy == academy)
              .Where(c => c.Grade == grade)
              .Where(c => c.ClassNum == num);
                return query.ToList();
            }

        }
        //根据专业年纪查找班级
        public List<CClass> GetCClassesByMajorGrade(string major, string grade, int? num)
        {
            if (num == null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major==major)
               .Where(c => c.Grade == grade);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .Where(c => c.Major==major)
              .Where(c => c.Grade == grade)
              .Where(c => c.ClassNum == num);
                return query.ToList();
            }
        }

        public CClass GetCClassesByMajorGradeNum(string major, string grade, int num)
        {
            var query = dbContext.CClass.SingleOrDefault
                (c => c.Major == major && c.Grade == grade && c.ClassNum == num);
            return query;
        }

        //根据班级ID查询班级信息 
        public double GetClassAvaScore(string id)
        {
            return GetClass(id).AvaragePoint;
        }



        public int GetClassNotSubNum(string id)
        {
            return GetClass(id).NotSubmitNum;
        }



        //排序
        //按班号排序
        public List<CClass> SortByClassNum(string major, string grade)
        {
            var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.ClassNum);
            return query.ToList();

        }

        //按平均综测成绩升序排序
        public List<CClass> SortByAvaPoint(string major,string grade)
        {
            if (major != null && grade != null)
            {
                var query = dbContext.CClass
                .Where(c => c.Major == major)
                .Where(c => c.Grade == grade)
                .OrderBy(c => c.AvaragePoint);
                return query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderBy(c => c.AvaragePoint);
                return query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.AvaragePoint);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.AvaragePoint);
                return query.ToList();
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
                return query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.AvaragePoint);
                return query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.AvaragePoint);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.AvaragePoint);
                return query.ToList();
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
                return query.ToList();
            }
            else if (academy != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Academy == academy)
               .OrderBy(c => c.NotSubmitNum);
                return query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderBy(c => c.NotSubmitNum);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderBy(c => c.NotSubmitNum);
                return query.ToList();
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
                return query.ToList();
            }
            else if (major != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Major == major)
               .OrderByDescending(c => c.NotSubmitNum);
                return query.ToList();
            }
            else if (grade != null)
            {
                var query = dbContext.CClass
               .Where(c => c.Grade == grade)
               .OrderByDescending(c => c.NotSubmitNum);
                return query.ToList();
            }
            else
            {
                var query = dbContext.CClass
              .OrderByDescending(c => c.NotSubmitNum);
                return query.ToList();
            }



        }


    }
}