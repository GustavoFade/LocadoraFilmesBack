using LocadoraFilmes.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Domain.Abstractions
{
    public interface IClienteRepository
    {
        Task<Cliente> FindAsync(Expression<Func<Cliente, bool>> predicate, bool onlyRead, CancellationToken cancellationToken);
        Task SaveAsync(Cliente entity, CancellationToken cancellationToken);
    }
}
