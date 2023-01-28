﻿using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = null!;
    }
}
