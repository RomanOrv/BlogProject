using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class EFArticleRepository : IArticleRepository
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


        public bool CheckUniqueTitle(string title)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                bool isExists = context.CreateObjectSet<Article>().Any(u => u.Title == title);
                return isExists ? false : true;
            };
        }


        public void AddNewArticle(string title)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var articles = context.CreateObjectSet<Article>();
                int maxId = articles.Any() ? articles.Max(x => x.Id) : 1;

                Article newArticle = new Article()
                {
                    Id = +maxId,
                    Title = title,
                    AuthorId = 2, //??????
                    Content = string.Empty,
                    CreationTime = DateTime.Now,
                    Published = true
                };

                articles.AddObject(newArticle);
                context.SaveChanges();
            };
        }



        public void SetArticleContent(int id, string content)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                Article article = context.CreateObjectSet<Article>().Single(x=> x.Id == id);
                article.Content = content;
                context.SaveChanges();
            }
        }


        public Article GetArticleForId(int id)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.CreateObjectSet<Article>().FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
