using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Infra.Data.Context;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraClientes.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IBaseRepository<Cliente, ApplicationDbContext> _baseRepository;
        public ClienteRepository(IBaseRepository<Cliente, ApplicationDbContext> baseRepository)
        {
            _baseRepository = baseRepository;
        }


        public async Task<Cliente> FindAsync(Expression<Func<Cliente, bool>> predicate, bool onlyRead, CancellationToken cancellationToken)
        {
            return await _baseRepository.FindAsync(predicate, onlyRead, cancellationToken);
        }

        public async Task SaveAsync(Cliente entity, CancellationToken cancellationToken)
        {
            await _baseRepository.SaveAsync(entity, cancellationToken);
        }
    }
}
