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

        private EFPictureRepository _pictureRepository;

        public EFUserRepository(string connectionString)
        {
            this._connectionString = connectionString;
            _pictureRepository = new EFPictureRepository(this._connectionString);
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
                User user = context.CreateObjectSet<User>().Single(x => x.Id == id);
                Picture picture = _pictureRepository.GetPicture(user.PictureId);
                data = picture.FileData;
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

        public void AddNewUser(string firstname, string surname, string email, string description, string username, string password, byte[] imagebyte, string imgtype)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var users = context.CreateObjectSet<User>();
                int maxId = users.Any() ? users.Max(x => x.Id) : 1;

                int pictureId = _pictureRepository.AddPicture(imagebyte, imgtype, string.Empty);

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
                    isEnable = true,
                    isAdmin = false,
                    PictureId = pictureId
                };

                users.AddObject(newUser);
                context.SaveChanges();
            };
        }


        public User GetUser(string login, string password)
        {
            using (ObjectContext context = new ObjectContext(this._connectionString))
            {
                return context.CreateObjectSet<User>().Single(r => r.Username == login && r.Password == password && r.isEnable == true);
            }
        }

        public User GetUser(string login)
        {
            using (ObjectContext context = new ObjectContext(this._connectionString))
            {
                return context.CreateObjectSet<User>().Single(r => r.Username == login);
            }
        }
    }
}
