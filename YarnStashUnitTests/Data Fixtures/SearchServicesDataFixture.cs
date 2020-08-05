using System;
using Microsoft.EntityFrameworkCore;
using YarnStash.Data;

namespace YarnStashUnitTests.DataFixtures
{
    public class SearchServicesDataFixture : IDisposable
    {
        public YarnContext context { get; set; }

        public SearchServicesDataFixture()
        {
            //creating test database
            var contextOptions = new DbContextOptionsBuilder<YarnContext>().UseInMemoryDatabase("inMemoryDatabase_SearchService").Options;
            context = new YarnContext(contextOptions);

            //seeding test database
            context.Yarn.Add(new YarnStash.Models.YarnModel { id = 1, Manufacturer = "Man. 1", Name = "Name 1" });
            context.Yarn.Add(new YarnStash.Models.YarnModel { id = 2, Manufacturer = "Man. 2", Name = "Name 2" });
            context.SaveChanges();

        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

