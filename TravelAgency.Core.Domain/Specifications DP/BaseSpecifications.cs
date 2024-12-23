using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;
using TravelAgency.Core.Domain.Specifications;

namespace TravelAgency.Core.Domain.Specifications_DP
{
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity> 
        where TEntity : Entity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set ; }
        public Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get ; set ; }


        protected BaseSpecifications()
        {
        }

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression) 
        {
            Criteria = CriteriaExpression;

        }

        protected BaseSpecifications(int id) 
        {
            Criteria = E => E.Id.Equals(id);
        }

        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
    }
}
