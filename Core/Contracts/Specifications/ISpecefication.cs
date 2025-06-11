using Domain.Entites;
using System.Linq.Expressions;

namespace Application.Contracts.Specifications
{
     public interface ISpecefication<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public Expression<Func<T, object>>? OrderBy { get;}
        public Expression<Func<T, object>>? OrderByDescending { get; }
    }
}
