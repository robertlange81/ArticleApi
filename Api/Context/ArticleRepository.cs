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

        public async Task<IEnumerable<Article>> GetAllArticles()
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

        public async Task<IEnumerable<Article>> GetArticleByArticleId(string articleId)
        {
            using (var connection = _context.CreateConnection())
            {
                var articleDictionary = new Dictionary<long, Article>();
                var articles = await connection.QueryAsync<Article, Translation, Article>(
                    "dbo.spArticle_GetByArticleID",
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
                    commandType: CommandType.StoredProcedure,
                    param: new { articleId }
                );
                return articles.Distinct().ToList();
            }
        }

        public async Task<IEnumerable<Article>> GetArticleByTitleAndLastModified(string title, DateTime? lastModifiedFrom, DateTime? lastModifiedTo)
        {
            using (var connection = _context.CreateConnection())
            {
                var articleDictionary = new Dictionary<long, Article>();
                var articles = await connection.QueryAsync<Article, Translation, Article>(
                    "dbo.spArticle_GetByTitleAndLastModified",
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
                    commandType: CommandType.StoredProcedure,
                    param: new {
                        title,
                        lastModifiedFrom,
                        lastModifiedTo
                    }
                );
                return articles.Distinct().ToList();
            }
        }
    }
}
