using EvaluationSystem.Services;
using EvaluationSystem.Models;
using EvaluationSystem.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace EvaluationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext userDb;
        private readonly UserService userService;

        public UserController(UserDbContext userDb) 
        {  
            this.userDb = userDb;
            userService = new UserService(userDb);
        }


        //用户登录
        [HttpGet]
        public ActionResult<String> CheckLogin(string id,string pwd,int type)
        {
            var user=userService.CheckLogin(id,pwd,type);
            if(user==null)
            {
                return BadRequest("不存在该用户或用户密码错误！");
            }
            return user.UserId;
        }

        //增加用户
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            try
            {
                if (userService.GetUser(user.UserId) == null)
                {
                    userService.AddUser(user);
                }
                else
                {
                    return BadRequest("该用户已经存在！");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return user;

        }

        //创建用户信息
        [HttpPost("StuId")]
        public ActionResult AddStuInfo(StudentInfo student)
        {
            try
            {
                userService.AddInfo(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //创建用户信息
        [HttpPost("TutorId")]
        public ActionResult AddTutorInfo(TutorInfo tutor)
        {
            try
            {
                userService.AddInfo(tutor);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //创建用户信息
        [HttpPost("SuperAdminId")]
        public ActionResult AddTutorInfo(SuperAdminInfo superAdminInfo)
        {
            try
            {
                userService.AddInfo(superAdminInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //删除指定id用户
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            try
            {
                //级联删除用户信息表里的信息
                userService.DeleteInfo(id);
                //删除user表里的user
                userService.RemoveUser(id);

            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //修改指定id用户账号信息
        [HttpPut("{id}")]
        public ActionResult<User> ModifyUser(string id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                userService.ModifyUser(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //修改用户具体信息
        [HttpPut("stuInfo/{stuid}")]
        public ActionResult<StudentInfo> ModifyStuInfo(string stuid,StudentInfo studentInfo)
        {
            if(stuid!=studentInfo.UserId)
            {
                return BadRequest("用户信息ID不可更改！");
            }
            userService.ModifyInfo(studentInfo);
            return studentInfo;
        }

        [HttpPut("tutorInfo/{tutorid}")]
        public ActionResult<TutorInfo> ModifyTutorInfo(string tutorid, TutorInfo tutorInfo)
        {
            if (tutorid != tutorInfo.UserId)
            {
                return BadRequest("用户信息ID不可更改！");
            }
            userService.ModifyInfo(tutorInfo);
            return tutorInfo;
        }

        [HttpPut("superadminInfo/{superadminid}")]
        public ActionResult<SuperAdminInfo> ModifySuperAdminInfo(string superadminid, SuperAdminInfo superAdminInfo)
        {
            if (superadminid != superAdminInfo.UserId)
            {
                return BadRequest("用户信息ID不可更改！");
            }
            userService.ModifyInfo(superAdminInfo);
            return superAdminInfo;
        }






        //根据用户id查找用户
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            User user;
            try
            {
                user = userService.GetUser(id);
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException);
            }
            return user;
        }

        //根据用户姓名查找用户
        [HttpGet("name")]
        public ActionResult<List<User>> GetUserByName(string name)
        {
            
            try
            {
                var users = userService.QueryUserByName(name);
                if (users != null)
                {
                    return users;
                }
                else
                {
                    return BadRequest("未找到符合要求的用户！");
                }
            }catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //根据用户类型查找用户
        [HttpGet("type")]
        public ActionResult<List<User>> GetUserByType(int type)
        {

            try
            {
                var users = userService.QueryUserByType(type);
                if (users != null)
                {
                    return users;
                }
                else
                {
                    return BadRequest("未找到符合要求的用户！");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //查找用户信息
        [HttpGet("stu")]
        public ActionResult<StudentInfo> GetStudentInfo(string id)
        {
            var studentinfo = userService.QueryStuInfo(id);
            if(studentinfo==null)
            {
                return BadRequest("不存在该用户信息！");
            }
            return studentinfo;
        }

        [HttpGet("tutor")]
        public ActionResult<TutorInfo> GetTutorInfo(string id)
        {
            var tutorinfo = userService.QueryTutorInfo(id);
            if (tutorinfo == null)
            {
                return BadRequest("不存在该用户信息！");
            }
            return tutorinfo;
        }
        [HttpGet("SuperAdmin")]
        public ActionResult<SuperAdminInfo> GetSuperAdminInfo(string id)
        {
            var superadmininfo = userService.QuerySuperAdminInfo(id);
            if (superadmininfo == null)
            {
                return BadRequest("不存在该用户信息！");
            }
            return superadmininfo;
        }















    }









}

