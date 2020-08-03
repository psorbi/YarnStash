using System;
using Microsoft.EntityFrameworkCore;
using YarnStash.Data;

namespace YarnStashUnitTests.DataFixtures
{
    //make a disposable data fixture for use in unit tests
    public class YarnSeedDataFixture : IDisposable
    {
        public YarnContext context { get; set; }

        public YarnSeedDataFixture()
        {
            //creating test database
            var contextOptions = new DbContextOptionsBuilder<YarnContext>().UseInMemoryDatabase("inMemoryDatabase").Options;
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
