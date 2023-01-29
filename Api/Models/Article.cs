
using Microsoft.IdentityModel.Tokens;
using System;

namespace Api.Models
{
    public class Article
    {
        private string articleId;

        public long Id { get; set; }
        public string ArticleId { get => articleId; set => articleId = value.Trim(); }
        public string? Color { get; set; }
        public bool isBulky { get; set; }
        public bool IsApproved
        {
            get
            {
                int CountryCount = Enum.GetNames(typeof(CountryCode)).Length;
                return CountryCount == Translations.Count();
            }
        }
        public Dictionary<string, Translation> Translations { get; set; }
    }
}
