using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSystem.Services
{
    public class ComEvaService
    {
        EvaDbContext DbContext;
        public ComEvaService(EvaDbContext DbContext)
        {
            this.DbContext = DbContext;
            classService = new ClassService(DbContext);
            userService = new UserService(DbContext);
        }

        ClassService classService;
        UserService userService;

        //添加
        public ComEvaluation AddComEvalution(ComEvaluation evaluation)
        {
            DbContext.ComEvaluations.Add(evaluation);
            DbContext.SaveChanges();
            return evaluation;
        }

        public ComEvaluation AddComEvalutionByStuIfo(string stuid, string grade,
                             double F3C1 = 10, double F3C2 = 10, double F3C3 = 10, double F3C4 = 10, double F3C5 = 10,
                              double F2B2rate = 0.002, double F1rate = 0.10, double F2rate = 0.7, double F3rate = 0.20,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)
        {
            ComEvaluation com = new ComEvaluation(stuid, grade);
            com.Intialize(F3C1, F3C2, F3C3, F3C4, F3C5, F2B2rate, F1rate, F2rate, F3rate, F1n, F2n2, F3n);
            StudentInfo stuinfo = userService.QueryStuInfo(stuid);
            User user = userService.GetUser(stuid);
            CClass cClass = classService.GetClass(stuinfo.ClassId);
            com.ClassId = stuinfo.ClassId;
            com.Major = stuinfo.Major;
            com.Name = user.Name;
            com.ClassNum = cClass.ClassNum;
            DbContext.ComEvaluations.Add(com);
            double  num = com.F1MaxScore;
            for(int i = 0; i < com.F1num; i++)
            {
                ComEvaAn an = new ComEvaAn(num,com);
                com.A.Add(an);
                DbContext.comEvaAns.Add(an);
                DbContext.SaveChanges();
            }
            
            return com;
        }

        //发布综测表以及初始化
        public List<ComEvaluation> AddComEvalutionAll(string major,string grade,string tutorid,string nowgrade, 
                             double F3C1 = 10, double F3C2 = 10, double F3C3 = 10, double F3C4 = 10, double F3C5 = 10,
                              double F2B2rate = 0.002, double F1rate = 0.10, double F2rate = 0.7, double F3rate = 0.20,
                              int F1n = 5, int F2n2 = 8, int F3n = 5)
        {
            List<string> stu = userService.QueryStuInfo(major, grade,tutorid);
            List<ComEvaluation> com = new List<ComEvaluation>();
            foreach(string s in stu)
            {
                com.Add(AddComEvalutionByStuIfo(s, nowgrade, F3C1, F3C2, F3C3, F3C4, F3C5, F2B2rate, F1rate, F2rate, F3rate, F1n, F2n2, F3n));
            }
            return com;
            
        }

        //填写综测表
        //F1
        //学生填写
        public ComEvaluation ModifyComEvaF1Self(ComEvaluation com, List<ComEvaAn> a)
        {
            for(int i = 0; i < a.Count(); i++)
            {
                com.WriteF1Self(i, a[i].ASelf, a[i].ARemark);
            }
            DbContext.SaveChanges();
            return com;
        }

        //辅导员填写
        public ComEvaluation ModifyComEvaF1Audit(ComEvaluation com, List<ComEvaAn> a)
        {
            for (int i = 0; i < a.Count(); i++)
            {
                com.WriteF1Audit(i, a[i].AAudit);
            }
            DbContext.SaveChanges();
            return com;
        }

        //F2
        //添加必修课到综测表中
        public ComEvaluation AddrequiredData(string StudentId, string grade, List<StudentCourseMark> requiredData)
        {
            var com = DbContext.ComEvaluations.SingleOrDefault(c => c.UserId == StudentId && c.Grade == grade);
            com.AddF2B1(requiredData);
            DbContext.SaveChanges();
            return com;
        }

        //添加选修课到综测表中
        public ComEvaluation AddselectedData(string StudentId, string grade,List<StudentCourseMark> selectedData)
        {
            var com = DbContext.ComEvaluations.SingleOrDefault(c => c.UserId == StudentId && c.Grade == grade);

            com.AddF2B2(selectedData);
            DbContext.SaveChanges();
            return com;
        }

        public ComEvaluation ModifyComEvaF2Score(ComEvaluation com, List<StudentCourseMark> sm, List<StudentCourseMark> sn)
        {
           //List<StudentCourseMark> sm = scmark.GetStudentCourseMark(StudentId, grade, semester, "专业必修课", null);
            double ff1= sm.Sum(b => b.stuMark * b.coursePoint) / (sm.Sum(b => b.coursePoint) == 0 ? 1 : sm.Sum(b => b.coursePoint));
            com.ffaudit = com.ffaudit-com.F2rate*(com.ff2b1 -ff1);
            com.ffself = com.ffself - com.F2rate * (com.ff2b1 - ff1);
            com.ff2b1 = Math.Round(ff1,3);
            //List<StudentCourseMark> sn = scmark.GetStudentCourseMark(StudentId, grade, semester, "专业选修课", null);
            sn.Sort((m, n) => Convert.ToInt16(m.stuMark * m.coursePoint - n.stuMark * n.coursePoint));
            List<StudentCourseMark> sw = new List<StudentCourseMark>();
            int num = Math.Min(com.F2B2num, sn.Count());
            for (int i = 0; i <num; i++)
            {
                sw.Add(sn[i]);
            }
            double ff2 = com.F2B2rate * (sw.Sum(b => b.stuMark * b.coursePoint));
            com.ffself = com.ffself-com.F2rate*(com.ff2b2-ff2);
            com.ffaudit =com.ffaudit - com.F2rate * (com.ff2b2 - ff2);
            com.ff2b2 = Math.Round(ff2,3);
            com.ffself = Math.Round(com.ffself, 3);
            com.ffaudit = Math.Round(com.ffaudit, 3);
            DbContext.SaveChanges();
            return com;

        }

        //F3
        //添加加分项到综测表中
        public ComEvaluation AddBenefit(string StudentId, string grade, List<ExtraBenefit> e)
        {
            var com=DbContext.ComEvaluations.SingleOrDefault(c => c.UserId==StudentId && c.Grade==grade);
            com.AddF3(e);
            DbContext.SaveChanges();
            return com;
        }


        //查询
        //学生综测单的操作
        //总表
        //通过学生Id和学期显示
        public List<ComEvaluation> QueryComEvaByStudent(string studentid, string grade)
        {
            if (grade != "全部")
            {
                return DbContext.ComEvaluations
                    .Where(c => c.UserId == studentid)
                    .Where(c => c.Grade == grade)
                    .ToList();
            }
            else
            {
                return DbContext.ComEvaluations
                    .Where(c => c.UserId == studentid)
                    .ToList();
            }
        }
        //通过综测表id查找
        public ComEvaluation QueryComEvaById(string id)
        {
            var com=DbContext.ComEvaluations
                .Include(c=>(c as ComEvaluation).A)
                .Include(c => (c as ComEvaluation).ExtBenList)
                //.Include()
                .SingleOrDefault(c => c.ComEvaId == id);
            return com;
        }
            

        //辅导员的综测单显示
        //通过studentid的list查询
        public List<ComEvaluation> QueryComEvaByList(List<string> list)
        {
            return DbContext.ComEvaluations
                .Where(c => list.Contains(c.UserId))
                .ToList();
        }

        //个人学号排序
        public List<ComEvaluation> QueryComEvaByListOrderId(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Where(c => list.Contains(c))
                .OrderBy(c => c.UserId)
                .ToList();
        }
        //个人成绩升序
        public List<ComEvaluation> QueryComEvaByListOrderGradeUp(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Where(c => list.Contains(c))
                .OrderBy(c => c.ffaudit)
                .ToList();
        }
        //个人成绩降序
        public List<ComEvaluation> QueryComEvaByListOrderGradeDown(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Where(c => list.Contains(c))
                .OrderByDescending(c => c.ffaudit)
                .ToList();
        }

        //班级表
        //根据审核状态筛选
        public List<ComEvaluation> QueryComEvaByState(List<ComEvaluation> list, string state)
        {
            if (state != "all")
            {
                return DbContext.ComEvaluations
                    .Where(c => list.Contains(c))
                    .Where(c => c.State == state)
                    .ToList();
            }
            else
            {
                return list;
            }
        }

        //根据输入的字段筛选
        public List<ComEvaluation> QueryComEvaByword(List<ComEvaluation> list, string ?word)
        {
            if (word != null)
            {
                List<User> U = userService.QueryUserByName(word);
                List<string> name = new List<string>();
                foreach (User u in U)
                {
                    name.Add(u.Name);
                }
                name.Add(word);
                return DbContext.ComEvaluations
                    .Where(c => list.Contains(c))
                    .Where(c => name.Contains(c.UserId))
                    .ToList();
            }
            else
            {
                return list;
            }

        }

        //根据class返回综测列表
        public List<ComEvaluation> QueryComEvaByClassId(CClass C)
        {
            return DbContext.ComEvaluations
                .Where(c => c.ClassId == C.CClassId)
                .ToList();
        }


        //给List<ComEvaluation> 返回未提交人数
        public int QueryUnSubmit(List<ComEvaluation> com)
        {
            return com.FindAll(c => c.State == "未填报").Count();
        }

        //给List<ComEvaluation> 返回平均分
        public double QueryAVEGrade(List<ComEvaluation> com)
        {
            return (com.FindAll(c => c.State == "已过审").Sum(c => c.F1Audit)) / (com.Count()==0?1:com.Count());
        }

        //修改每个班级的未交人数和平均分
        public void ModifyClassIfo(List<CClass> cclass)
        {
            foreach (CClass c in cclass)
            {
                List<ComEvaluation> com = QueryComEvaByClassId(c);
                int num = QueryUnSubmit(com);
                double avg = QueryAVEGrade(com);
                classService.ModifyAvaPoint(c.CClassId, avg);
                classService.ModifyNotSubNum(c.CClassId, num);
                DbContext.SaveChanges();
            }
        }

        //修改综测表状态
        public ComEvaluation ModifyStateSucess(string ComEvaId)
        {
            var com = QueryComEvaById(ComEvaId);
            com.auditsucc();
            DbContext.SaveChanges();
            return com;
        }
        public ComEvaluation ModifyStateSubmit(string ComEvaId)
        {
            var com = QueryComEvaById(ComEvaId);
            com.submit();
            DbContext.SaveChanges();
            return com;
        }

        public ComEvaluation ModifyStateFail(string ComEvaId)
        {
            var com = QueryComEvaById(ComEvaId);
            com.auditfail();
            DbContext.SaveChanges();
            return com;
        }

        public void DeleteComEvalution(string id)
        {
            var com = DbContext.ComEvaluations
                .Include(c => c.A)
                .SingleOrDefault(c => c.ComEvaId == id);
            if (com == null) return;
            DbContext.comEvaAns.RemoveRange(com.A);
            DbContext.ComEvaluations.Remove(com);
            DbContext.SaveChanges();
        }

    }
}
