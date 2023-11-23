using AutoMapper;
using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using LocadoraFilmes.Application.Exceptions;
using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        public GeneroService(IGeneroRepository generoRepository, IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int idGenero, CancellationToken cancellationToken)
        {
            var genero = await _generoRepository.FindAsync(x => x.Id == idGenero, true, cancellationToken);
            if (genero is null)
                throw new NotFoundException("Genero não encontrado!");
            await _generoRepository.DeleteAsync(genero);
        }

        public async Task<GeneroResponseDto[]> FindAllAsync(CancellationToken cancellationToken)
        {
            var generos = await _generoRepository.FindAllAsync(true, cancellationToken);

            return _mapper.Map<GeneroResponseDto[]>(generos);
        }

        public async Task<GeneroResponseDto> FindAsync(int idGenero, CancellationToken cancellationToken)
        {
            var genero = await _generoRepository.FindAsync(x => x.Id == idGenero, true, cancellationToken);
            if (genero is null)
                throw new NotFoundException("Genero não encontrado!");
            return _mapper.Map<GeneroResponseDto>(genero);
        }

        public async Task<GeneroResponseDto> SaveAsync(GeneroRequestDto generoDto, CancellationToken cancellationToken)
        {
            var genero = _mapper.Map<Genero>(generoDto);

            var generoEntity = await _generoRepository.SaveAsync(genero, cancellationToken);

            return _mapper.Map<GeneroResponseDto>(generoEntity);
        }

        public async Task UpdateAsync(int idGenero, GeneroRequestDto generoDto)
        {
            var genero = _mapper.Map<Genero>(generoDto);
            genero.ChangeIdGenero(idGenero);
            await _generoRepository.UpdateAsync(genero);
        }
    }
}
