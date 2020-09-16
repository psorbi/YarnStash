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
        public IQueryable<PatternModel> SortPattern(IQueryable<PatternModel> patternModel, string sortOrder)
        {
            IQueryable<PatternModel> patterns;
            patterns = patternModel.OrderBy(p => p.Name.ToLower());
            return patterns;
        }

        //TO DO: update search method for more accurate results
        public IQueryable<PatternModel> SearchByInput(IQueryable<PatternModel> patternModels, string searchString)
        {
            IQueryable<PatternModel> patterns;

            patterns = patternModels.Where(y => y.Designer.ToLower().Contains(searchString.ToLower())
                    || y.Name.ToLower().Contains(searchString.ToLower()));

            return patterns;
        }

        public IQueryable<YarnModel> SortYarn(IQueryable<YarnModel> yarnModels, string sortOrder)
        {
            IQueryable<YarnModel> yarns;


            //sort table by column, default is manufacterer ascending
            switch (sortOrder)
            {
                case "manufacturer_desc":
                    yarns = yarnModels.OrderByDescending(y => y.Manufacturer.ToLower());
                    break;
                case "Name":
                    yarns = yarnModels.OrderBy(y => y.Name.ToLower());
                    break;
                case "name_desc":
                    yarns = yarnModels.OrderByDescending(y => y.Name.ToLower());
                    break;
                case "Amount":
                    yarns = yarnModels.OrderBy(y => y.Amount);
                    break;
                case "amount_desc":
                    yarns = yarnModels.OrderByDescending(y => y.Amount);
                    break;
                case "Size":
                    yarns = yarnModels.OrderBy(y => y.Size);
                    break;
                case "size_desc":
                    yarns = yarnModels.OrderByDescending(y => y.Size);
                    break;
                default:
                    yarns = yarnModels.OrderBy(y => y.Manufacturer.ToLower());
                    break;
            }


            return yarns;
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
