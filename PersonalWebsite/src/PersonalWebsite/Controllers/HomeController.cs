using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalWebsite.Github;
using PersonalWebsite.Models;
using PersonalWebsite.Tests.Models;
using PersonalWebsite.Tests.Models.Enums;
using PersonalWebsite.ViewModels;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new GithubClient("AlexGipp");
            var repos = await client.GetPublicRepos(Type.Owner, Sort.Created);

            var viewModel = new IndexViewModel
            {
                Repos = (List<Repo>) repos.Where(x => x.Fork == false).ToList()
            };

            return View(viewModel);
        }
        [Authorize(AuthenticationSchemes = "Github")] //todo remove oauth parts OwO
        public IActionResult Privacy()
        {
            var id = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var name = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            
            if(id != "33844665")
                return Unauthorized();
            
            return View();
        }

        public IActionResult GithubAuthFailed()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}