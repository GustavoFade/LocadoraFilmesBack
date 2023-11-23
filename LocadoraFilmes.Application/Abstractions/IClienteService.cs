using LocadoraFilmes.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Abstractions
{
    public interface IClienteService
    {
        Task<ClienteDto> FindAsync(ClienteDto clienteDto, CancellationToken cancellationToken);
        Task<ClienteDto> SaveAsync(ClienteDto clienteDto, CancellationToken cancellationToken);
    }
}
