using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.Data;

namespace EvaluationSystem.Services
{
    public class EvaRuleService
    {
        EvaDbContext dbContext;
        public EvaRuleService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //根据学院年级返回规则对象
        public EvaRule GetRule(string academy,string grade)
        {
            var query = dbContext.rules.FirstOrDefault(r => r.Academy == academy && r.Grade == grade);
            if(query!=null)
            {
                return query;
            }
            else
            {
                throw new ArgumentException("不存在匹配加分规则！");
            }

        }
    }
}
