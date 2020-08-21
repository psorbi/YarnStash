using System;
using System.Linq;
using YarnStash.Models;

namespace YarnStash.Interfaces
{
    public interface ISearchServices
    {
        IQueryable<YarnModel> SortYarn(IQueryable<YarnModel> yarnModels, string sortOrder);
        IQueryable<YarnModel> SearchByInput(IQueryable<YarnModel> yarnModels, string searchString);
    }
}
