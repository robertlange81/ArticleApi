using Api.Models;
using Dapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Specialized;
using System.Data;

namespace Api.Context
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DapperContext _context;
        public ArticleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            using (var connection = _context.CreateConnection())
            {
                var articleDictionary = new Dictionary<long, Article>();
                var articles = await connection.QueryAsync<Article, Translation, Article>(
                    "dbo.spArticle_GetAll",
                    (article, translation) => {
                        if (!articleDictionary.TryGetValue(article.Id, out Article docEntry))
                        {
                            docEntry = article;
                            docEntry.Translations = new Dictionary<string, Translation>();
                            articleDictionary.Add(docEntry.Id, docEntry);
                        }

                        if (translation != null) docEntry.Translations.Add(translation.CountryCode, translation);

                        return docEntry;
                    },
                    splitOn: "CountryCode",
                    commandType: CommandType.StoredProcedure
                );
                return articles.Distinct().ToList();
            }
        }
    }
}
