using System;
using System.Linq;
using YarnStash.Interfaces;
using YarnStash.Models;

namespace YarnStash.Services
{
    public class SearchServices : ISearchServices

    {
        public SearchServices()
        {
        }

        //TO DO: update search method for more accurate results
        public IQueryable<YarnModel> SearchByInput(IQueryable<YarnModel> yarnModels, string searchString)
        {
            IQueryable<YarnModel> yarns;

            yarns = yarnModels.Where(y => y.Manufacturer.ToLower().Contains(searchString.ToLower())
                    || y.Name.ToLower().Contains(searchString.ToLower()));

            return yarns;
        }

    }
}
