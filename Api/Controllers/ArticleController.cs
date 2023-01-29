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
    }
}
