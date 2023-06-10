using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using EvaluationSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CClassController : ControllerBase
    {
        private readonly EvaDbContext classDb;
        private readonly ClassService classService;

        public CClassController(EvaDbContext classDb)
        {
            this.classDb = classDb;
            classService = new ClassService(classDb);
        }

        //增加班级
        [HttpPost]
        public ActionResult<CClass> Add(CClass cClass)
        {
            try
            {
                classService.AddClass(cClass);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();

        }
        [HttpGet("{id}")]
        public ActionResult<CClass> getCClass(int id)
        {
            try
            {
                return classService.GetClass(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        //按班号排序
        [HttpGet("classnum")]
        public ActionResult<List<CClass>> SortByClassNum(string? major, string? grade)
        {
            try
            {
                return classService.SortByClassNum(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }



        //根据专业年级查找班级
        [HttpGet("query")]
        public ActionResult<List<CClass>> QueryByMG(string major, string grade, int? num)
        {
            try
            {
                List<CClass> classinfo = classService.GetCClassesByMajorGrade(major, grade, num);
                return classinfo;
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        //获取班级平均分
        [HttpGet("evapoint")]
        public ActionResult<double> GetEvaPoint(int id)
        {
            try
            {
                return classService.GetClassAvaScore(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        //获取未提交人数
        [HttpGet("notsubnum")]
        public ActionResult<int> GetNotSubNum(int id)
        {
            try
            {
                return classService.GetClassNotSubNum(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //获取学业预警人数
        [HttpGet("precautionnum")]
        public ActionResult<int> GetPrecautionNum(int id, string major, string grade)
        {
            try
            {
                return classService.QueryPrecautionNum(id, major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }


        //平均综测成绩升序
        [HttpGet("sort/evapoint")]
        public ActionResult<List<CClass>> SortByEvaPoint(string? major, string? grade)
        {
            try
            {
                return classService.SortByAvaPoint(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //平均综测成绩降序
        [HttpGet("sort/evapoint/descend")]
        public ActionResult<List<CClass>> SortByEvaPointDescend(string? major, string? grade)
        {
            try
            {
                return classService.SortByAvaPointDescend(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //未提交人数升序
        [HttpGet("sort/notsubnum")]
        public ActionResult<List<CClass>> SortByNotSubNum(string? major, string? grade)
        {
            try
            {
                return classService.SortByNotSubNum(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //未提交人数降序
        [HttpGet("sort/notsubnum/descend")]
        public ActionResult<List<CClass>> SortByNotSubNumDescend(string? major, string? grade)
        {
            try
            {
                return classService.SortByNotSubNumDescend(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //学业预警人数升序
        [HttpGet("sort/precautionnum")]
        public ActionResult<List<CClass>> SortByPreCautionNum(string? major, string? grade)
        {
            try
            {
                return classService.SortByPrecautionNum(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //学业预警人数降序
        [HttpGet("sort/precautionnum/descend")]
        public ActionResult<List<CClass>> SortByPreCautionNumDescend(string? major, string? grade)
        {
            try
            {
                return classService.SortByPrecautionNumDescend(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //GPA升序
        [HttpGet("sort/avggpa")]
        public ActionResult<List<CClass>> SortByAvgGpa(string? major, string? grade)
        {
            try
            {
                return classService.SortByAvgGpa(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }
        //GPA降序
        [HttpGet("sort/avggpa/descend")]
        public ActionResult<List<CClass>> SortByAvgGpaDescend(string? major, string? grade)
        {
            try
            {
                return classService.SortByAvgGpaDescend(major, grade);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        //修改班级信息
        [HttpPut]
        public ActionResult Modify(CClass c)
        {
            try
            {
                classService.ModifyClass(c);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();

        }

        //修改班级平均分
        [HttpPut("evgPoint")]
        public ActionResult ModifyEvaPoint(int id, double point)
        {
            try
            {
                classService.ModifyAvaPoint(id, point);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        //修改班级未提交综测人数
        [HttpPut("notsubnum")]
        public ActionResult Modifynotsubnum(int id, int num)
        {
            try
            {
                classService.ModifyNotSubNum(id, num);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }


        //修改班级学业预警人数

        [HttpPut("precautionnum")]
        public IActionResult modifyPrecautionNum(string grade, string major, int classnum, int modifynum)
        {
            try
            {
                classService.ModifyPrecautionNum(grade, major, classnum, modifynum);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //修改班级平均GPA
        [HttpPut("avggpa")]
        public ActionResult ModifyAvgGpa(string grade, string major, int classnum, double gpa)
        {
            try
            {
                classService.ModifyAvgGpa(grade, major, classnum, gpa);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }




    }
}
