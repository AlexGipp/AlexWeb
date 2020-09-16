using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.Github;
using PersonalWebsite.Tests.Models;
using PersonalWebsite.Tests.Models.Enums;
using Type = PersonalWebsite.Tests.Models.Enums.Type;

namespace PersonalWebsite.Tests
{
    public class GithubTests
    {
        [Test]
        public async Task GetAllRepos_ValidUsername_ReturnRepos()
        {
            //Arrange
            var client = new GithubClient("AlexGipp");
            
            //Act
            var repos = await client.GetAllRepos(Type.Owner, Sort.Created);
            
            //Assert
            repos.Should().NotBeNullOrEmpty();
        }
    }
}