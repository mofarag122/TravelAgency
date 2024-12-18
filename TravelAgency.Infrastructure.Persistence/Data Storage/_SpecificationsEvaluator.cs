using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;
using TravelAgency.Core.Domain.Specifications;

namespace TravelAgency.Infrastructure.Persistence.Data_Storage
{
    public static class _SpecificationsEvaluator<TEntity> where TEntity : Entity 
    {
        public static IEnumerable<TEntity> LambdaExpressionsBuilder(IEnumerable<TEntity> entities , ISpecifications<TEntity> specifications)
        {
            if (specifications.Criteria is not null)
            {
                var predicate = specifications.Criteria.Compile();
                entities = entities.Where(predicate);
            }

            if (specifications.OrderBy is not null)
            {
                var orderBy = specifications.OrderBy.Compile();
                entities = entities.OrderBy(orderBy);
            }

            if(specifications.OrderByDesc is not null)
            {
                var orderByDesc = specifications.OrderByDesc.Compile();
                entities = entities.OrderByDescending(orderByDesc);
            }
                


            return entities;

        }
    }
}
