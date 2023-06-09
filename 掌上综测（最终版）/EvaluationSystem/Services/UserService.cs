﻿using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Asn1.Mozilla;

namespace EvaluationSystem.Services
{
    public class UserService
    {
        EvaDbContext dbContext;
        public UserService(EvaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public void AddUser(User user)
        {

            dbContext.Users.Add(user);
            dbContext.SaveChanges();



        }

        public void RemoveUser(string userId)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("不存在该用户！");
            }

        }

        //根据userId查找用户
        public User GetUser(string userId)
        {
            return dbContext.Users.FirstOrDefault(x => x.UserId == userId);
        }

        //根据用户姓名查找用户
        public List<User> QueryUserByName(string username)
        {
            var query = dbContext.Users
                .Where(user => user.Name == username);
            return query.ToList();
        }

        //根据用户类型查找用户
        public List<User> QueryUserByType(int type)
        {
            var query = dbContext.Users
                .Where(user => user.Type == type);
            return query.ToList();
        }

        //年级 班级 专业 辅导员查找学生信息
        //专业年级

        public List<string> QueryStuInfo(string grade, string major, string tutorid)
        {
            List<string> list = new();
            if (grade != "all" && major != "all")
            {
                var query = dbContext.Students
                    .Where(s => s.Grade == grade)
                    .Where(s => s.TutorId == tutorid)
                    .Where(s => s.Major == major);
                List<StudentInfo> l = query.ToList();
                if (l != null)
                {
                    for (int i = 0; i < l.Count(); i++)
                    {
                        list.Add(l[i].UserId);
                    }
                }
                else
                {
                    throw new Exception("不存在该用户！");
                }
            }
            else
            {
                if (major == "all" && grade == "all")
                {
                    var query = dbContext.Students
                        .Where(s => s.TutorId == tutorid);
                    List<StudentInfo> l = query.ToList();
                    if (l != null)
                    {
                        for (int i = 0; i < l.Count(); i++)
                        {
                            list.Add(l[i].UserId);
                        }
                    }
                    else
                    {
                        throw new Exception("不存在该用户！");
                    }
                }
                if (grade == "all")
                {
                    var query = dbContext.Students
                        .Where(s => s.Major == major)
                        .Where(s => s.TutorId == tutorid);
                    List<StudentInfo> l = query.ToList();
                    if (l != null)
                    {
                        for (int i = 0; i < l.Count(); i++)
                        {
                            list.Add(l[i].UserId);
                        }
                    }
                    else
                    {
                        throw new Exception("不存在该用户！");
                    }
                }
                if (major == "all")
                {
                    var query = dbContext.Students
                        .Where(s => s.Grade == grade)
                        .Where(s => s.TutorId == tutorid);
                    List<StudentInfo> l = query.ToList();
                    if (l != null)
                    {
                        for (int i = 0; i < l.Count(); i++)
                        {
                            list.Add(l[i].UserId);
                        }
                    }
                    else
                    {
                        throw new Exception("不存在该用户！");
                    }
                }
            }
            return list;
        }

        //根据班级id查找学生list
        public List<string> QueryStuInfoByClassId(int classid)
        {
            List<string> list = new();
            var query = dbContext.Students
               .Where(s => s.ClassId == classid)
               .ToList();
            foreach (StudentInfo s in query)
            {
                list.Add(s.UserId);
            }
            return list;
        }


        //修改用户信息
        public void ModifyUser(User newuser)
        {
            RemoveUser(newuser.UserId);
            AddUser(newuser);
        }

        //登录
        public User CheckLogin(string id, string pwd, int type)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id
                                                    && x.Password == pwd
                                                    && x.Type == type);
            return user;

        }

        //创建用户相关信息
        public void AddInfo(StudentInfo student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }
        public void AddInfo(TutorInfo tutor)
        {
            dbContext.Tutors.Add(tutor);
            dbContext.SaveChanges();
        }
    


        //根据用户id删除用户信息
        public void DeleteInfo(string id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                switch (user.Type)
                {
                    case 0:
                        var stu = dbContext.Students.FirstOrDefault(x => x.UserId == id);
                        if (stu != null)
                        {
                            dbContext.Students.Remove(stu);
                            dbContext.SaveChanges();
                        }
                        break;
                    case 1:
                        var tutor = dbContext.Tutors.FirstOrDefault(x => x.UserId == id);
                        if (tutor != null)
                        {
                            dbContext.Tutors.Remove(tutor);
                            dbContext.SaveChanges();
                        }
                        break;   
                    default:
                        break;
                }
            }
        }
        //修改用户具体信息
        public void ModifyInfo(StudentInfo studentInfo)
        {
            DeleteInfo(studentInfo.UserId);
            AddInfo(studentInfo);
        }

        public void ModifyInfo(TutorInfo tutorInfo)
        {
            DeleteInfo(tutorInfo.UserId);
            AddInfo(tutorInfo);
        }

        

        //查找信息
        public StudentInfo QueryStuInfo(string id)
        {

            return dbContext.Students.FirstOrDefault(s => s.UserId == id);
        }

        public TutorInfo QueryTutorInfo(string id)
        {
            return dbContext.Tutors.FirstOrDefault(t => t.UserId == id);
        }

      



    }
}