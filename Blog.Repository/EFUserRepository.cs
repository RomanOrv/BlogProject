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
                return context.CreateObjectSet<User>().Single(x=>x.Id == id);
            }
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
    }
}
