using System;
using System.Linq;
using YarnStash.Models;

namespace YarnStash.Interfaces
{
    public interface ISearchServices
    {
        IQueryable<YarnModel> SearchByInput(IQueryable<YarnModel> yarnModels, string searchString);
    }
}
