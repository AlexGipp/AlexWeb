using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Database.Models;
using PersonalWebsite.Database.MongoDB;

namespace PersonalWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExperienceController : ControllerBase
    {
        [HttpGet("GetExperiences")]
        public async Task<object> GetExperiences()
        {
            var dataService = new DataService("PortfolioWebsite");
            var experiences = await dataService.LoadAllRecordsAsync<Experience>("experience");

            return experiences.ToList();
        }

        [HttpGet("GetExperienceById")]
        public async Task<object> GetExperiencesById(Guid id)
        {
            var dataService = new DataService("PortfolioWebsite");
            var experiences = await dataService.LoadRecordByIdAsync<Experience>("experience", id);

            return experiences;
        }

        [HttpPost("AddExperience")]
        public async Task AddExperience(string companyName, string jobDescription, DateTime startDate, DateTime endDate,
            string websiteUrl, string companyLogoUrl)
        {
            var exp = new Experience
            {
                CompanyName = companyName,
                JobDescription = jobDescription,
                StartDate = startDate.Date.ToLocalTime(),
                EndDate = endDate.Date.ToLocalTime(),
                WebsiteUrl = new Uri(websiteUrl),
                CompanyLogoUrl = new Uri(companyLogoUrl)
            };
            var dataService = new DataService("PortfolioWebsite");
            await dataService.InsertRecordAsync("experience", exp);
        }

        [HttpPost("DeleteExperience")]
        public async Task DeleteExperience(Guid id)
        {
            var dataService = new DataService("PortfolioWebsite");
            await dataService.DeleteRecordAsync<Experience>("experience", id);
        }

        [HttpPost("UpdateExperience")]
        public async Task UpdateExperience(Guid id,
            string companyName,
            string jobDescription,
            DateTime startDate,
            DateTime endDate,
            string websiteUrl,
            string companyLogoUrl)
        {
            var exp = new Experience
            {
                Id = id,
                CompanyName = companyName,
                JobDescription = jobDescription,
                StartDate = startDate.Date.ToLocalTime(),
                EndDate = endDate.Date.ToLocalTime(),
                WebsiteUrl = new Uri(websiteUrl),
                CompanyLogoUrl = new Uri(companyLogoUrl)
            };

            var dataService = new DataService("PortfolioWebsite");
            await dataService.UpsertRecordAsync("experience", id, exp);
        }
    }
}