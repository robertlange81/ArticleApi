using System;

namespace Api.Models
{
    public class Translation
    {
        public string ArticleId { get; set; }
        public string CountryCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
