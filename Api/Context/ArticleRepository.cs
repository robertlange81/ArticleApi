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
                title = title.Trim();
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

        public async Task<bool> InsertArticle(Article article)
        {
            // TODO prevent dulicates / catch exceptions due to already existing primary key
            using (var connection = _context.CreateConnection())
            {
                int success = await connection.ExecuteAsync(
                    "dbo.[spArticle_Insert]",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        article.ArticleId,
                        article.Color,
                        article.isBulky
                    }
                );
                return success > 0;
            }
        }

        public async Task<IEnumerable<string>> GetAllStopwords()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<string>(
                    $"Select term from dbo.[ArticleStopword];"
                );
            }
        }

        public async Task<bool> InsertStopword(string term)
        {
            // TODO prevent dulicates / catch exceptions due to already existing primary key
            using (var connection = _context.CreateConnection())
            {
                int success = await connection.ExecuteAsync(
                    // TODO: SQL Escape to prevent injection
                    $"Insert into dbo.[ArticleStopword] ([Term]) values ('" + term + "');"
                );
                return success > 0;
            }
        }

        public async Task<bool> InsertTranslation(Translation translation)
        {
            // TODO prevent dulicates / catch exceptions due to already existing primary key
            using (var connection = _context.CreateConnection())
            {
                int success = await connection.ExecuteAsync(
                    "dbo.[spTranslation_Insert]",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        translation.ArticleId,
                        translation.CountryCode,
                        translation.Title,
                        translation.Description
                    }
                );
                return success > 0;
            }
        }
    }
}
