using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Abstractions
{
    public interface IGeneroService
    {
        Task<GeneroResponseDto> FindAsync(int idGenero, CancellationToken cancellationToken);
        Task<GeneroResponseDto[]> FindAllAsync(CancellationToken cancellationToken);
        Task<GeneroResponseDto> SaveAsync(GeneroRequestDto clienteDto, CancellationToken cancellationToken);
        Task UpdateAsync(int idGenero, GeneroRequestDto clienteDto);
        Task DeleteAsync(int idGenero, CancellationToken cancellationToken);
    }
}
