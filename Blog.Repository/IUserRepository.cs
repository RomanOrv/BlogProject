using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        void UpdateUser(int id, bool isEnable);
        bool CheckUnicueUsername(string username);
        void AddNewUser(string firstname, string surname, string email, string description, string username, string password, byte[] imagebyte, string imgtype);

        User GetUser(string login, string password);
        User GetUser(string login); 
    }
}
