using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public EFUserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.CreateObjectSet<User>().ToList();
            }
        }

        public User GetUser(int id)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.CreateObjectSet<User>().Single(x => x.Id == id);
            }
        }


        public byte[] GetContentImage(int id)
        {
            byte[] data;
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
              data =  context.CreateObjectSet<User>().Single(x=>x.Id == id).imgFile;
            }
            return data;
        }





        public void UpdateUser(int id, bool isEnable)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                User user = context.CreateObjectSet<User>().Single(x => x.Id == id);
                user.isEnable = isEnable;
                context.SaveChanges();
            }
        }

        public bool CheckUnicueUsername(string username)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                bool isExists = context.CreateObjectSet<User>().Any(x => x.Username == username);
                return isExists ? false : true;
            }
        }

        public void AddNewUser(string firstname, string surname, string email, string description, string username, string password, byte[] imagebyte, string fileName)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var users = context.CreateObjectSet<User>();
                int maxId = users.Any() ? users.Max(x => x.Id) : 1;

                User newUser = new User()
                {
                    Id = +maxId,
                    Firstname = firstname,
                    Surname = surname,
                    Email = email,
                    DateRegister = DateTime.Now,
                    Description = description,
                    Username = username,
                    Password = password,
                    imgFile = imagebyte,
                    imgFileName = fileName
                };

                users.AddObject(newUser);
                context.SaveChanges();
            };
        }
    }
}
