using System;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalWebsite.Database.Models
{
    public class Experience
    {
        [BsonId] 
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Uri WebsiteUrl { get; set; }
        public Uri CompanyLogoUrl { get; set; }
    }
}