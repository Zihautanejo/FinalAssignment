﻿using EvaluationSystem.Services;
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
        public ActionResult<User> CheckLogin(string id,string pwd,int type)
        {
            var user=userService.CheckLogin(id,pwd,type);
            if(user==null)
            {
                return BadRequest("不存在该用户或用户密码错误！");
            }
            return user;
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
                var users = userService.QueryUser(name);
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
        public ActionResult<List<User>> GetUserByName(int type)
        {

            try
            {
                var users = userService.QueryUser(type);
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


        //增加用户
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            try
            {
                if(userService.GetUser(user.UserId)==null)
                {
                    userService.AddUser(user);
                }
                else
                {
                    return BadRequest("该用户已经存在！");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return user;

        }


        //修改指定id用户信息
        [HttpPut("{id}")]
        public ActionResult<User> PutUser(string id,User user)
        {
            if(id!=user.UserId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                userService.ModifyUser(user);
            }
            catch(Exception e) 
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
                //删除user表里的user
                userService.RemoveUser(id);
                //级联删除用户信息表里的信息
                userService.DeleteInfo(id);
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

        //创建用户信息
        [HttpPost("StuId")]
        public ActionResult AddStuInfo(StudentInfo student)
        {
            try
            {
                userService.AddInfo(student);
            }
            catch(Exception e)
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




    }









}
