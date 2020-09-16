using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pathoschild.Http.Client;
using PersonalWebsite.Tests.Models;
using PersonalWebsite.Tests.Models.Enums;
using Type = PersonalWebsite.Tests.Models.Enums.Type;

namespace PersonalWebsite.Github
{
    public class GithubClient
    {
        public GithubClient(string username)
        {
            _client = new FluentClient($"https://api.github.com/users/{username}");
        }

        private readonly FluentClient _client;

        public async Task<IList<Repo>> GetPublicRepos(Type type, Sort sort)
        {
            try
            {
                var repos = await _client
                    .GetAsync("repos")
                    .WithArgument("type", type)
                    .WithArgument("sort", sort)
                    .As<IList<Repo>>();

                return repos;
            }
            catch (ApiException  ex)
            {
                string responseText = await ex.Response.AsString();
                throw new Exception($"The API responded with HTTP {ex.Response.Status}: {responseText}");
            }
           
        }
    }
}