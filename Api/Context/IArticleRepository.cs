using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Api.Models;

namespace Api.Context
{
    public interface IArticleRepository
    {
        public Task<IEnumerable<Article>> GetArticles();
    }
}
