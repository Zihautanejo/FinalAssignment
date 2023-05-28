using System.Collections.Generic;
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
        }

        ClassService classService;
        UserService userService;

        //添加
        public void AddComEvalution(ComEvaluation evaluation)
        {
            DbContext.ComEvaluations.Add(evaluation);
            DbContext.SaveChanges();
        }

        //学生综测单的操作
        //总表
        //通过学生Id和学期显示
        public List<ComEvaluation> QueryComEvaByStudent(string studentid, string semester)
        {
            return DbContext.ComEvaluations
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => c.UserId == studentid)
                .Where(c => c.Semester ==semester)
                .ToList();
        }

        //辅导员的综测单显示
        //通过studentid的list查询
        public List<ComEvaluation> QueryComEvaByList(List<string> list)
        {
            return DbContext.ComEvaluations
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => list.Contains(c.UserId))
                .ToList();
        }

        //个人学号排序
        public List<ComEvaluation> QueryComEvaByListOrderId(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => list.Contains(c))
                .OrderBy(c => c.UserId)
                .ToList();
        }
        //个人成绩升序
        public List<ComEvaluation> QueryComEvaByListOrderGradeUp(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => list.Contains(c))
                .OrderBy(c => c.FAudit)
                .ToList();
        }
        //个人成绩降序
        public List<ComEvaluation> QueryComEvaByListOrderGradeDown(List<ComEvaluation> list)
        {
            return DbContext.ComEvaluations
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => list.Contains(c))
                .OrderByDescending(c => c.FAudit)
                .ToList();
        }

        //班级表
        //根据审核状态筛选
        public List<ComEvaluation> QueryComEvaByState(List<ComEvaluation> list, string state)
        {
            if (state != null)
            {
                return DbContext.ComEvaluations
                    .Include(c => c.A)
                    .Include(c => c.B1)
                    .Include(c => c.B2)
                    .Include(c => c.F3)
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
        public List<ComEvaluation> QueryComEvaByword(List<ComEvaluation> list, string word)
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
                    .Include(c => c.A)
                    .Include(c => c.B1)
                    .Include(c => c.B2)
                    .Include(c => c.F3)
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
                .Include(c => c.A)
                .Include(c => c.B1)
                .Include(c => c.B2)
                .Include(c => c.F3)
                .Where(c => c.ClassId == C.ClassId)
                .ToList();
        }


        //给List<ComEvaluation> 返回未提交人数
        public int QueryUnSubmit(List<ComEvaluation> com)
        {
            return com.FindAll(c => c.State == "0").Count();
        }

        //给List<ComEvaluation> 返回平均分
        public double QueryAVEGrade(List<ComEvaluation> com)
        {
            return com.Sum(c => c.F1Audit) / com.Count();
        }

        //修改每个班级的未交人数和平均分
        public void ModifyClassIfo(List<CClass> cclass)
        {
            foreach (CClass c in cclass)
            {
                List<ComEvaluation> com = QueryComEvaByClassId(c);
                int num = QueryUnSubmit(com);
                double avg = QueryAVEGrade(com);
                classService.ModifyAvaPoint(c.ClassId, avg);
                classService.ModifyNotSubNum(c.ClassId, num);
            }
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