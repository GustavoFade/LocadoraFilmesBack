using AutoMapper;
using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using LocadoraFilmes.Application.Exceptions;
using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        public FilmeService(IFilmeRepository filmeRepository, IMapper mapper, IGeneroRepository generoRepository)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
            _generoRepository = generoRepository;
        }

        public async Task DeleteAsync(int idFilme, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.FindAsync(x => x.Id == idFilme, cancellationToken);
            if (filme is null)
                throw new NotFoundException("Filme não encontrado!");
            await _filmeRepository.DeleteAsync(filme, cancellationToken);
        }

        public async Task DeleteManyAsync(List<int> idsFilme, CancellationToken cancellationToken)
        {
            var filmes = await _filmeRepository.FindAllByIdAsync(idsFilme, cancellationToken);
            var filmesNaoExistentes = idsFilme.Where(x => !filmes.Select(y => y.Id).Contains(x)).ToList();
            if (filmesNaoExistentes.Count > 0)
            {
                string idsInformadosNaoExistente = string.Join(",", filmesNaoExistentes);
                throw new NotFoundException($"Filmes não encontrados! Ids informados não existem: {idsInformadosNaoExistente}");
            }

            await _filmeRepository.DeleteManyAsync(filmes, cancellationToken);
        }

        public async Task<FilmeResponseDto[]> FindAllAsync(CancellationToken cancellationToken)
        {
            var filmes = await _filmeRepository.FindAllAsync(true, cancellationToken);

            return _mapper.Map<FilmeResponseDto[]>(filmes);
        }

        public async Task<FilmeResponseDto> FindAsync(int idFilme, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.FindAsync(x => x.Id == idFilme, cancellationToken);
            if (filme is null)
                throw new NotFoundException("Filme não encontrado!");
            return _mapper.Map<FilmeResponseDto>(filme);
        }

        public async Task<FilmeResponseDto> SaveAsync(FilmeRequestDto filmeDto, CancellationToken cancellationToken)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            var generos = await _generoRepository.FindAllByIdAsync(filmeDto.IdsGenero, cancellationToken);
            filme.AdicionarGeneros(generos);

            var filmeEntity = await _filmeRepository.SaveAsync(filme, cancellationToken);

            return _mapper.Map<FilmeResponseDto>(filmeEntity);
        }

        public async Task UpdateAsync(int idFilme, FilmeRequestDto filmeDto, CancellationToken cancellationToken)
        {
            var filmeEntity = await _filmeRepository.FindAsync(x => x.Id == idFilme, cancellationToken);
            if (filmeEntity is null)
                throw new NotFoundException("Filme não encontrado!");

            var generos = await _generoRepository.FindAllByIdAsync(filmeDto.IdsGenero, cancellationToken);
            var generosNaoExistentes = generos.Where(x => !filmeEntity.Generos.Select(g => g.Id).Contains(x.Id)).ToList();

            filmeEntity.AtualizarEntidade(filmeDto.Nome, filmeDto.Ativo, generosNaoExistentes);

            await _filmeRepository.UpdateAsync(filmeEntity);
        }
    }
}
