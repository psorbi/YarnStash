using System;
using System.Linq;
using YarnStash.Models;

namespace YarnStash.Interfaces
{
    public interface ISearchServices
    {
        IQueryable<PatternModel> SortPattern(IQueryable<PatternModel> patternModels, string sortOrder);
        IQueryable<PatternModel> SearchByInput(IQueryable<PatternModel> patternModels, string searchString);
        IQueryable<YarnModel> SortYarn(IQueryable<YarnModel> yarnModels, string sortOrder);
        IQueryable<YarnModel> SearchByInput(IQueryable<YarnModel> yarnModels, string searchString);
    }
}
