using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Api.Models;

namespace Api.Context
{
    public interface IArticleRepository
    {
        public Task<IEnumerable<Article>> GetAllArticles();
        public Task<IEnumerable<Article>> GetArticleByArticleId(string articleId);
        public Task<IEnumerable<Article>> GetArticleByTitleAndLastModified(string title, DateTime? lastModifiedFrom, DateTime? lastModifiedTo);
    }
}
