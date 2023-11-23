using LocadoraFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Abstractions
{
    public interface IGeneroRepository
    {
        Task<Genero> FindAsync(Expression<Func<Genero, bool>> predicate, bool onlyRead, CancellationToken cancellationToken);

        Task<List<Genero>> FindAllByIdAsync(List<int> idsGenero, CancellationToken cancellationToken);

        Task<Genero[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken);

        Task<Genero> SaveAsync(Genero entity, CancellationToken cancellationToken);

        Task UpdateAsync(Genero entity);

        Task DeleteAsync(Genero genero);
    }
}
