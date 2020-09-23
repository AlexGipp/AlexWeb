using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.Database.Models;
using PersonalWebsite.Database.MongoDB;

namespace PersonalWebsite.Tests
{
    public class DatabaseTests
    {
        [Test]
        public async Task InsertRecord_ValidInformation_AddOneRecord()
        {
            //Arrange
            var dataService = new DataService("PortfolioWebsite");
            var exp = new Experience
            {
                CompanyName = "Recy Systems",
                JobDescription = "blablablab",
                StartDate = new DateTime(2019, 9, 1),
                EndDate = new DateTime(2022, 9, 1),
                WebsiteUrl = new Uri("http://recy-systems.com/"),
                CompanyLogoUrl = new Uri("https://www.recy-systems.com/files/design-data/rsag_amcs_logo.svg")
            };

            //Act
            await dataService.InsertRecordAsync("experience", exp);

            //Assert
            Assert.IsNotNull(dataService.LoadAllRecordsAsync<Experience>("experience"));
        }

        [Test]
        public async Task LoadAllRecords_ValidTableGiven_ReturnInfo()
        {
            //Arrange
            var dataService = new DataService("PortfolioWebsite");

            //Act
            var results = await dataService.LoadAllRecordsAsync<Experience>("experience");

            //Assert
            results.Should().BeOfType<List<Experience>>();
        }

        [Test]
        public void LoadRecordById_ValidIdGiven_ReturnNothing()
        {
            //Arrange
            var dataService = new DataService("PortfolioWebsite");

            //Act
            Func<Task> act = async () =>
            {
                await dataService.LoadRecordByIdAsync<Experience>("experience",
                    new Guid("00000000-0000-0000-0000-000000000000"));
            };

            //Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Sequence contains no elements");
        }

        [Test]
        public async Task UpsertRecord_IdGiven_ShouldNotReturnNull()
        {
            //Arrange
            var dataService = new DataService("PortfolioWebsite");
            var anyEntry = await dataService.LoadAllRecordsAsync<Experience>("experience");

            //Act
            await dataService.UpsertRecordAsync("experience", anyEntry.First().Id, anyEntry.First());
            var allEntries = await dataService.LoadAllRecordsAsync<Experience>("experience");
            var newEntry = allEntries.Find(e => e.Id == anyEntry.First().Id);

            //Assert
            newEntry.Should().NotBeNull().And.BeOfType<Experience>();
        }

        [Test]
        public async Task DeleteRecord_IdGiven_OneLessEntry()
        {
            //Arrange
            var dataService = new DataService("PortfolioWebsite");
            var exp = new Experience
            {
                CompanyName = "Recy Systems",
                JobDescription = "blablablab",
                StartDate = new DateTime(2019, 9, 1),
                EndDate = new DateTime(2022, 9, 1),
                WebsiteUrl = new Uri("http://recy-systems.com/"),
                CompanyLogoUrl = new Uri("https://www.recy-systems.com/files/design-data/rsag_amcs_logo.svg")
            };

            //Act
            await dataService.InsertRecordAsync("experience", exp);
            var oldAllEntries = await dataService.LoadAllRecordsAsync<Experience>("experience");
            await dataService.DeleteRecordAsync<Experience>("experience", oldAllEntries.First().Id);
            var newAllEntries = await dataService.LoadAllRecordsAsync<Experience>("experience");

            //Assert
            Assert.AreEqual(newAllEntries.Count, oldAllEntries.Count - 1);
        }
    }
}