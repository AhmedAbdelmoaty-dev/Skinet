using Application.Contracts.Specifications;
using Domain.Entites;

namespace Infrastructure.Specification
{
    internal class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T>GetQuery(IQueryable<T> inputQuery,ISpecefication<T> spec)
        {
            if(spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }
            if(spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
            }
            if(spec.isPagingEnabled)
            {
                inputQuery = inputQuery.Skip(spec.skip).Take(spec.take);
            }
            return inputQuery;
        }
    }
}
