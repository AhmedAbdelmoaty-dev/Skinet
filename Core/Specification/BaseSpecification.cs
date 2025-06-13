using Application.Contracts.Specifications;
using Domain.Entites;
using System.Linq.Expressions;


namespace Application.Specification
{
    public class BaseSpecification<T> : ISpecefication<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int skip { get; private set; }

        public int take { get; private set; }

        public bool isPagingEnabled { get; private set; }

        public BaseSpecification(Expression<Func<T,bool>>criteria)
        {
            Criteria = criteria;
        }

        protected void AddOrderByAsc(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }
        protected void ApplyPaging(int skip, int take)
        {
            this.skip = skip;
            this.take = take;
            isPagingEnabled = true;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query=query.Where(Criteria);
            }
            return query;   
        }
    }
}
