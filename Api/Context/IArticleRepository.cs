using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Api.Models;

namespace Api.Context
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<IEnumerable<Article>> GetArticleByArticleId(string articleId);
        Task<IEnumerable<Article>> GetArticleByTitleAndLastModified(string title, DateTime? lastModifiedFrom, DateTime? lastModifiedTo);
        Task<bool> InsertArticle(Article article);
        Task<bool> InsertStopword(string term);
        Task<IEnumerable<string>> GetAllStopwords();
        Task<bool> InsertTranslation(Translation translation);
    }
}
