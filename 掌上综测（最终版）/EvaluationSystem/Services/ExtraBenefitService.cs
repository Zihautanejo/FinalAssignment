using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EvaluationSystem.Services
{
    public class ExtraBenefitService
    {
        EvaDbContext dbContext;
        private readonly ComEvaService comEvaService;
        private readonly UserService userService;
        public ExtraBenefitService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.comEvaService = new ComEvaService(dbContext);
            this.userService = new UserService(dbContext);
        }

        //增加
        public void AddExtraBenefit(ExtraBenefit extraBenefit)
        {
            var stu = userService.QueryStuInfo(extraBenefit.StuId);
            var comeva = comEvaService.QueryComEvaByStudent(stu.UserId, stu.Grade);
            extraBenefit.comeva = comeva[0];
            dbContext.extraBenefits.Add(extraBenefit);
            dbContext.SaveChanges();
        }

        //删除
        public void RemoveExtraBenefit(int id)
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
            RemoveExtraBenefit(extraBenefit.ExtraBenefitId);
            AddExtraBenefit(extraBenefit);
        }

        //查询
        //根据学生id查找额外加分
        public List<ExtraBenefit> GetExtraBenefit(string id)
        {
            var query= dbContext.extraBenefits
                .Where(b => b.StuId == id);
            if(query!=null)
            {
                return query.ToList();
            }
            else
            {
                throw new ArgumentException("不存在加分项！");
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


        //根据专业，年级，审批状态，加分类型，辅导员ID
        public List<ExtraBenefit> GetBenefitsTutor(string? major,string? grade,string? status,string? type,string id)
        {
            if(major!=null)
            {
                if(grade!=null)
                {
                    if(status!=null)
                    {
                        if(type!=null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major && studentinfo.Grade == grade
                                        && benefit.BenefitStatus == status && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if(query!=null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                            
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major && studentinfo.Grade == grade
                                        && benefit.BenefitStatus == status 
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                    //status=null
                    else
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major && studentinfo.Grade == grade
                                        && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major && studentinfo.Grade == grade
                                        
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                }
                //grade=null
                else
                {
                    if (status != null)
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major 
                                        && benefit.BenefitStatus == status && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major
                                        && benefit.BenefitStatus == status
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                    //status=null
                    else
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major
                                        && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Major == major 

                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                }
            }
            //major=null
            else
            {
                if (grade != null)
                {
                    if (status != null)
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Grade == grade
                                        && benefit.BenefitStatus == status && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Grade == grade
                                        && benefit.BenefitStatus == status
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                    //status=null
                    else
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Grade == grade
                                        && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where studentinfo.Grade == grade

                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                }
                //grade=null
                else
                {
                    if (status != null)
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where benefit.BenefitStatus == status && benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where  benefit.BenefitStatus == status
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                    //status=null
                    else
                    {
                        if (type != null)
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where benefit.BenefitType == type
                                        && benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                        //type
                        else
                        {
                            var query = from benefit in dbContext.extraBenefits
                                        join studentinfo in dbContext.Students on benefit.StuId equals studentinfo.UserId
                                        where benefit.TutorId == id
                                        select benefit;
                            if (query != null)
                            {
                                return query.ToList();
                            }
                            else
                            {
                                throw new ArgumentException("不存在加分项！");
                            }
                        }
                    }
                }
            }
                      
            
        }


        //根据加分类型，状态，加分主题，学生id查询
        public List<ExtraBenefit> GetBenefits(string ?type,string ?status,string? Btheme,string id)
        {
            if(type!=null)
            {
                if (Btheme != null)
                {
                    if (status != null)
                    {
                        var query = dbContext.extraBenefits
                             .Where(b => b.BenefitType == type && b.BenefitTheme.Contains(Btheme) && b.BenefitStatus == status && b.StuId == id);
                        return query ==null?throw new ArgumentException("不存在相应加分项！"): query.ToList();
                    }
                    else
                    {
                       var query= dbContext.extraBenefits
                           .Where(b => b.BenefitType == type && b.BenefitTheme.Contains(Btheme) && b.StuId == id)
                           .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();


                    }
                }
                else
                {
                    if (status != null)
                    {
                        var query= dbContext.extraBenefits
                            .Where(b => b.BenefitType == type && b.BenefitStatus == status && b.StuId == id)
                            .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

                    }
                    else
                    {
                       var query= dbContext.extraBenefits
                           .Where(b => b.BenefitType == type && b.StuId == id)
                           .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

                    }
                }

            }
            else
            {
                if (Btheme != null)
                {
                    if (status != null)
                    {
                        var query= dbContext.extraBenefits
                            .Where(b => b.BenefitTheme.Contains(Btheme) && b.BenefitStatus == status && b.StuId == id)
                            .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

                    }
                    else
                    {
                        var query=dbContext.extraBenefits
                           .Where(b => b.BenefitTheme.Contains(Btheme) && b.StuId == id)
                           .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

                    }
                }
                else
                {
                    if (status != null)
                    {
                        var query= dbContext.extraBenefits
                            .Where(b =>  b.BenefitStatus == status && b.StuId == id)
                            .ToList();
                        return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

                    }
                    else
                    {
                        return QueryAll(id);
                    }
                }
            }
        }

        //返回该学生所有加分项
        public List<ExtraBenefit> QueryAll(string id)
        {
            var query=dbContext.extraBenefits.Where(x => x.StuId == id);
            return query == null ? throw new ArgumentException("不存在相应加分项！") : query.ToList();

        }

        //根据辅导员ID查询
        public List<ExtraBenefit> QueryByTutor(string id)
        {
            var query = dbContext.extraBenefits
                .Where(b => b.TutorId == id);
            if(query!=null)
            {
                return query.ToList();
            }
            else
            {
                throw new ArgumentException("不存在加分项！");
            }
        }
    }

    
}
