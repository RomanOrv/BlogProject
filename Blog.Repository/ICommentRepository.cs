using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
   public interface ICommentRepository
    {
       Comment[] GetArticleComments(int articleId);
        void AddNewComment(string content, int authorId, int articleId);
    }
}
