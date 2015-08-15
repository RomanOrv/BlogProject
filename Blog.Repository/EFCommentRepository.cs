using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class EFCommentRepository : ICommentRepository
    {
        private readonly string _connectionString;

        public EFCommentRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public Comment[] GetArticleComments(int articleId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.CreateObjectSet<Comment>()
                    .Where(u => u.ArticleId == articleId).ToArray();
            }
        }

        public void AddNewComment(string content, int authorId, int articleId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var comments = context.CreateObjectSet<Comment>();
                int maxId = comments.Any() ? comments.Max(x => x.Id) : 1;

                Comment comment = new Comment()
                {
                    Id = +maxId,
                    Content = content,
                    ArticleId = articleId,
                    AuthorId = authorId,
                    CommDate = DateTime.Now
                };
                comments.AddObject(comment);
                context.SaveChanges();
            }
        }
    }
}
