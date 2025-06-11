using Application.Contracts.Specifications;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specification
{
    public class BaseSpecification<T> : ISpecefication<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
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
    }
}
