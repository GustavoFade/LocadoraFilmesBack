using AutoMapper;
using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.Exceptions;
using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Abstractions.Security;
using LocadoraFilmes.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ICryptographyPassword _cryptographyPassword;
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper,
            ICryptographyPassword cryptographyPassword)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _cryptographyPassword = cryptographyPassword;
        }

        public async Task<ClienteDto> FindAsync(ClienteDto clienteDto, CancellationToken cancellationToken)
        {
            var cpf = clienteDto.Cpf;
            var senha = clienteDto.Senha;
            var senhaCriptografada = _cryptographyPassword.CriptografaHMACSHA256(senha, cpf);
            var cliente = await _clienteRepository.FindAsync(x => x.Cpf == clienteDto.Cpf && x.Senha == senhaCriptografada, true, cancellationToken);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> SaveAsync(ClienteDto clienteDto, CancellationToken cancellationToken)
        {
            var cpf = clienteDto.Cpf;
            var senha = clienteDto.Senha;
            var senhaCriptografada = _cryptographyPassword.CriptografaHMACSHA256(senha, cpf);

            var clienteEntity = await _clienteRepository.FindAsync(x => x.Cpf == clienteDto.Cpf, true, cancellationToken);
            if (clienteEntity is not null)
            {
                throw new AuthClienteExistenteRegisterException();
            }

            var cliente = new Cliente(cpf, senhaCriptografada);

            await _clienteRepository.SaveAsync(cliente, cancellationToken);
            return clienteDto;
        }
    }
}
