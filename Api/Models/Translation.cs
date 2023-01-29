using System;

namespace Api.Models
{
    public class Translation
    {
        private string _description;
        private string _title;

        public string ArticleId { get; set; }
        public string CountryCode { get; set; }
        public string Title { get => _title; set => _title = value.Trim(); }
        public string Description { get => _description; set => _description = value.Trim(); }
    }
}
