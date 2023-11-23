using AutoMapper;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using LocadoraFilmes.Domain.Entities;

namespace LocadoraFilmes.Application.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            MapCliente();
            MapFilme();
            MapGenero();
        }

        private void MapGenero()
        {
            CreateMap<GeneroResponseDto, Genero>()
                            .ReverseMap();
            CreateMap<GeneroRequestDto, Genero>()
                            .ReverseMap();
        }

        private void MapFilme()
        {
            CreateMap<FilmeRequestDto, Filme>()
                .ReverseMap();
            CreateMap<FilmeResponseDto, Filme>()
               .ReverseMap();
        }

        private void MapCliente()
        {
            CreateMap<ClienteDto, Cliente>()
                .ReverseMap();
        }
    }
}
