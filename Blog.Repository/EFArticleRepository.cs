using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
   public class EFArticleRepository: IArticleRepository
    {
        private readonly string _connectionString;
        public EFArticleRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<Article> GetPublished()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.CreateObjectSet<Article>()
                    .Where(u => u.Published == true)
                    .ToList();
            }
        }
    }
}
