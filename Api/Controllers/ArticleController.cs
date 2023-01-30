using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Context;

namespace Api.Controllers
{
    [Route("api/Articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepo;

        public ArticleController(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            try
            {
                var articles = await _articleRepo.GetAllArticles();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<Article>> GetArticleByArticleId(string articleId)
        {
            try
            {
                var articles = await _articleRepo.GetArticleByArticleId(articleId);
                if (articles.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("byTitleAndLastModified")]
        public async Task<ActionResult<Article>> GetArticleByTitle(string title, DateTime? lastModifiedFrom, DateTime? lastModifiedTo)
        {
            try
            {
                var articles = await _articleRepo.GetArticleByTitleAndLastModified(
                    title,
                    lastModifiedFrom,
                    lastModifiedTo
                );
                if (articles.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Stopwords/{term}")]
        public async Task<ActionResult<Article>> CreateStopWord(string term)
        {
            try
            {
                term = term.Trim();
                var stopwords = await _articleRepo.GetAllStopwords();
                if (stopwords.Any(sw => sw.Trim().Equals(term)))
                {
                    return Conflict("Term already exists.");
                }
                await _articleRepo.InsertStopword(term);
                return Created($"api/Stopwords/{term}", null);
            }
            // TODO prevent dulicates / catch exceptions due to already existing primary key
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{articleId}")]
        public async Task<ActionResult<Article>> CreateArticle(
            string articleId,
            string colorCode,
            bool isBulky)
        {
            try
            {
                var art = new Article();
                art.ArticleId = articleId;
                art.Color = colorCode;
                art.isBulky = isBulky;

                await _articleRepo.InsertArticle(art);
                // await _articleRepo.InsertTranslation(...);
                return Created("api/Articles/" + articleId, null);
            }
            // TODO prevent dulicates / catch exceptions due to already existing primary key
            catch (Exception ex)
            {
                //log error
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPatch("{articleId}")]
        public async Task<ActionResult<Article>> CreateTranslation(
            string articleId,
            string countryCode,
            string title,
            string description
        )
        {
            try
            {
                var trans = new Translation();
                trans.ArticleId = articleId;
                trans.CountryCode = countryCode;
                trans.Title = title;
                trans.Description = description;

                // TODO prevent dulicates / catch exceptions due to already existing primary key
                // TODO move validation in own method or class
                var stopwords = await _articleRepo.GetAllStopwords();
                if (
                    stopwords.Any(sw => trans.Title.Contains(sw.Trim()))
                )
                {
                    return UnprocessableEntity("Title contains forbidden words.");
                }

                if (
                    stopwords.Any(sw => trans.Description.Contains(sw.Trim()))
)
                {
                    return UnprocessableEntity("Description contains forbidden words.");
                }

                if (
                    !Enum.IsDefined(typeof(CountryCode), trans.CountryCode)
                )
                {
                    return UnprocessableEntity("Invalid CountryCode.");
                }

                var articles = await _articleRepo.GetArticleByArticleId(articleId);
                if (articles.Count() == 0)
                {
                    return NotFound();
                }

                if (articles.FirstOrDefault().Translations.ContainsKey(trans.CountryCode))
                {
                    // TODO: replace title and description instead
                    return UnprocessableEntity("CountryCode for Article already exists.");
                }

                await _articleRepo.InsertTranslation(
                    trans
                );
                return Created("api/Articles/" + articleId, null);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
