using EvaluationSystem.DAO;
using EvaluationSystem.Models;
using Org.BouncyCastle.Asn1.Mozilla;

namespace EvaluationSystem.Services
{
    public class UserService
    {
        UserDbContext dbContext;
        public UserService(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

      

        public void AddUser(User user)
        {

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            //级联增加具有id的用户信息
           /* switch (user.Type)
            {
                case 0:
                    StudentInfo studentInfo = new StudentInfo(user.UserId);
                    dbContext.Students.Add(studentInfo);
                    dbContext.SaveChanges();
                    break;
                case 1:
                    TutorInfo tutorInfo = new TutorInfo(user.UserId);
                    dbContext.Tutors.Add(tutorInfo);
                    dbContext.SaveChanges(); 
                    break;
                case 2:
                SuperAdminInfo superAdminInfo = new SuperAdminInfo(user.UserId);
                dbContext.SuperAdmins.Add(superAdminInfo);
                dbContext.SaveChanges();
                    break;
                default:
                    throw new ArgumentException();

            }*/


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
        public List<User> QueryUser(string username)
        {
            var query = dbContext.Users
                .Where(user => user.Name == username);
            return query.ToList();
        }

        //根据用户类型查找用户
        public List<User> QueryUser(int type)
        {
            var query = dbContext.Users
                .Where (user => user.Type == type);
            return query.ToList();  
        }

        //修改用户信息
         public void ModifyUser(User newuser)
        {
            RemoveUser(newuser.UserId);
            AddUser(newuser);
        }

        //登录
        public User CheckLogin(string id,string pwd,int type)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id
                                                    && x.Password == pwd
                                                    &&x.Type==type) ;
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
        public void AddInfo(SuperAdminInfo superAdmin)
        {
            dbContext.SuperAdmins.Add(superAdmin);
            dbContext.SaveChanges();
        }

  
        //根据用户id删除用户信息
        public void DeleteInfo(string id)
        {
            var user=dbContext.Users.FirstOrDefault(x=>x.UserId == id);
            if (user != null)
            {
                switch (user.Type)
                {
                    case 0:
                        var stu=dbContext.Students.FirstOrDefault(x=>x.StuId == id);
                        if(stu!=null)
                        {
                            dbContext.Students.Remove(stu);
                            dbContext.SaveChanges();
                        }
                        break;
                    case 1:
                        var tutor = dbContext.Tutors.FirstOrDefault(x => x.TutorId == id);
                        if (tutor != null)
                        {
                            dbContext.Tutors.Remove(tutor);
                            dbContext.SaveChanges();
                        }
                        break;
                    case 2:
                        var superAdmin = dbContext.SuperAdmins.FirstOrDefault(x => x.SuperAdminId == id);
                        if (superAdmin != null)
                        {
                            dbContext.SuperAdmins.Remove(superAdmin);
                            dbContext.SaveChanges();
                        }
                        break;
                    default:
                        break;
                }

                


            }
        }



    }
}
