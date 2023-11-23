using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {

        private readonly IBaseRepository<Genero, ApplicationDbContext> _baseRepository;
        public GeneroRepository(IBaseRepository<Genero, ApplicationDbContext> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(Genero genero)
        {
            await _baseRepository.DeleteAsync(genero);
        }

        public async Task<Genero[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken)
        {
            return await _baseRepository.FindAllAsync(onlyRead, cancellationToken);
        }

        public async Task<List<Genero>> FindAllByIdAsync(List<int> idsGenero, CancellationToken cancellationToken)
        {
            return await _baseRepository.GetContext()
                 .Set<Genero>().Where(x => idsGenero.Contains(x.Id.Value)).ToListAsync(cancellationToken);
        }

        public async Task<Genero> FindAsync(Expression<Func<Genero, bool>> predicate, bool onlyRead, CancellationToken cancellationToken)
        {
            return await _baseRepository.FindAsync(predicate, onlyRead, cancellationToken);
        }

        public async Task<Genero> SaveAsync(Genero entity, CancellationToken cancellationToken)
        {
            return await _baseRepository.SaveAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Genero entity)
        {
            await _baseRepository.UpdateAsync(entity);
        }
    }
}
