using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EvaluationSystem.Services
{
    public class ExtraBenefitService
    {
        EvaDbContext dbContext;
        public ExtraBenefitService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //增加
        public void AddExtraBenefit(ExtraBenefit extraBenefit)
        {
            dbContext.extraBenefits.Add(extraBenefit);
            dbContext.SaveChanges();
        }

        //删除
        public void RemoveExtraBenefit(string id)
        {
            var benefit = dbContext.extraBenefits.FirstOrDefault(b => b.ExtraBenefitId == id);
            if (benefit != null)
            {
                dbContext.extraBenefits.Remove(benefit);
                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("不存在改加分项");
            }
        }

        //修改
        public void ModifyExtraBenefit(ExtraBenefit extraBenefit)
        {

        }

        //查询
        //根据id查找额外加分
        public ExtraBenefit GetExtraBenefit(string id)
        {
            var query= dbContext.extraBenefits.FirstOrDefault(b => b.ExtraBenefitId == id);
            if(query!=null)
            {
                return query;
            }
            else
            {
                throw new ArgumentException("不存在该加分项！");
            }
        }

        //根据类型查询
      /*  public List<ExtraBenefit> GetExtraBenefits(string type)
        {
            var query = dbContext.extraBenefits
                .Where(b => b.BenefitType == type);
            return query.ToList();
        }

        //根据状态查询
        public List<ExtraBenefit> GetExtraBenefits(int status)
        {
            var query = dbContext.extraBenefits
                .Where(b => b.BenefitStatus == status);
            return query.ToList();
        }*/

        //根据加分类型，状态，加分主题查询
        public List<ExtraBenefit> GetBenefits(string ?type,string ?status,string? Btheme)
        {
            if(type!=null)
            {
                if (Btheme != null)
                {
                    if (status != null)
                    {
                        return dbContext.extraBenefits
                            .Where(b => b.BenefitType == type && b.BenefitTheme.Contains(Btheme) && b.BenefitStatus == status)
                            .ToList();
                    }
                    else
                    {
                        return dbContext.extraBenefits
                           .Where(b => b.BenefitType == type && b.BenefitTheme.Contains(Btheme))
                           .ToList();

                    }
                }
                else
                {
                    if (status != null)
                    {
                        return dbContext.extraBenefits
                            .Where(b => b.BenefitType == type && b.BenefitStatus == status)
                            .ToList();
                    }
                    else
                    {
                        return dbContext.extraBenefits
                           .Where(b => b.BenefitType == type)
                           .ToList();
                    }
                }

            }
            else
            {
                if (Btheme != null)
                {
                    if (status != null)
                    {
                        return dbContext.extraBenefits
                            .Where(b => b.BenefitTheme.Contains(Btheme) && b.BenefitStatus == status)
                            .ToList();
                    }
                    else
                    {
                        return dbContext.extraBenefits
                           .Where(b => b.BenefitTheme.Contains(Btheme))
                           .ToList();
                    }
                }
                else
                {
                    if (status != null)
                    {
                        return dbContext.extraBenefits
                            .Where(b =>  b.BenefitStatus == status)
                            .ToList();
                    }
                    else
                    {
                        return QueryAll();
                    }
                }
            }
        }

        //返回所有加分项
        public List<ExtraBenefit> QueryAll()
        {
            return dbContext.extraBenefits.ToList(); 
        }
    }
}
