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
    public class FilmeRepository : IFilmeRepository
    {
        private readonly IBaseRepository<Filme, ApplicationDbContext> _baseRepository;
        public FilmeRepository(IBaseRepository<Filme, ApplicationDbContext> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(Filme filme, CancellationToken cancellationToken)
        {
            await _baseRepository.DeleteAsync(filme);
        }

        public async Task DeleteManyAsync(List<Filme> filmes, CancellationToken cancellationToken)
        {
            _baseRepository.GetContext().Set<Filme>().RemoveRange(filmes);
            await Task.FromResult(0);
        }

        public async Task<Filme[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken)
        {
            return
                await _baseRepository.GetContext()
                .Set<Filme>()
                .Include(x => x.Generos)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Filme> FindAsync(Expression<Func<Filme, bool>> predicate, bool onlyRead, CancellationToken cancellationToken)
        {
            return await _baseRepository.FindAsync(predicate, onlyRead, cancellationToken);
        }

        public async Task<Filme> FindAsync(Expression<Func<Filme, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _baseRepository.GetContext()
                .Set<Filme>()
                .AsNoTracking()
                .Include(x => x.Generos)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<List<Filme>> FindAllByIdAsync(List<int> idsFilme, CancellationToken cancellationToken)
        {
            return await _baseRepository.GetContext()
                 .Set<Filme>().Where(x => idsFilme.Contains(x.Id.Value)).ToListAsync(cancellationToken);
        }

        public async Task<Filme> SaveAsync(Filme entity, CancellationToken cancellationToken)
        {
            return await _baseRepository.SaveAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Filme entity)
        {
            await _baseRepository.UpdateAsync(entity);
        }
    }
}
