using LocadoraFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Abstractions
{
    public interface IFilmeRepository
    {
        Task<Filme> FindAsync(Expression<Func<Filme, bool>> predicate, bool onlyRead, CancellationToken cancellationToken);

        Task<Filme> FindAsync(Expression<Func<Filme, bool>> predicate, CancellationToken cancellationToken);

        Task<Filme[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken);

        Task<List<Filme>> FindAllByIdAsync(List<int> idsFilme, CancellationToken cancellationToken);

        Task<Filme> SaveAsync(Filme entity, CancellationToken cancellationToken);

        Task UpdateAsync(Filme entity);

        Task DeleteAsync(Filme filme, CancellationToken cancellationToken);

        Task DeleteManyAsync(List<Filme> filmes, CancellationToken cancellationToken);
    }
}
