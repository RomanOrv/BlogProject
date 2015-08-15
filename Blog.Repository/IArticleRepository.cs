using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IArticleRepository
    {
        List<Article> GetPublished();
        bool CheckUniqueTitle(string title);
        void AddNewArticle(string title, int authorId);
        Article GetArticleForId(int id);
        void SetArticleContent(int id, string content);
        List<Article> GetUserArticles(int userId);
        void ChangePublishingStatus(Article article);
    }
}
