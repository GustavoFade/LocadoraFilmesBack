using LocadoraFilmes.Domain.Entities.Common;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Abstractions.Common
{
    public interface IBaseRepository<TEntity, TContext> where TEntity : Entity
    {

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool onlyRead, CancellationToken cancellationToken);

        Task<TEntity[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken);

        Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        TContext GetContext();
    }
}