using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Abstractions
{
    public interface IFilmeService
    {
        Task<FilmeResponseDto> FindAsync(int idFilme, CancellationToken cancellationToken);
        Task<FilmeResponseDto[]> FindAllAsync(CancellationToken cancellationToken);
        Task<FilmeResponseDto> SaveAsync(FilmeRequestDto clienteDto, CancellationToken cancellationToken);
        Task UpdateAsync(int idFilme, FilmeRequestDto clienteDto, CancellationToken cancellationToken);
        Task DeleteAsync(int idFilme, CancellationToken cancellationToken);
        Task DeleteManyAsync(List<int> idFilme, CancellationToken cancellationToken);
    }
}
