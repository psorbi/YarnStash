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
            context.Yarn.Add(new YarnStash.Models.YarnModel { id = 1, Manufacturer = "Berroco", Name = "Remix Light" });
            context.Yarn.Add(new YarnStash.Models.YarnModel { id = 2, Manufacturer = "Lily", Name = "Sugar 'n Cream" });
            context.SaveChanges();

        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
