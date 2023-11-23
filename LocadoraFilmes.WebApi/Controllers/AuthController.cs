using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.DTOs;
using LocadoraFilmes.Application.DTOs.ExceptionDTO;
using LocadoraFilmes.Application.Exceptions;
using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Abstractions.Security;
using LocadoraFilmes.Domain.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteService _clienteService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork,
                              IClienteService clienteService, ITokenProvider tokenProvider)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _clienteService = clienteService;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Fazer login na aplicação(obter o token que seja usado nas outras rotas)
        /// </summary>
        /// <param name="clienteDto">Objeto contendo informações do cliente para o login</param>
        /// <returns>
        /// Um token de acesso JWT válido, que pode ser usado nas outras rotas da aplicação.
        /// Exemplo:
        /// 
        /// <code>
        /// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
        /// </code>
        /// </returns>
        /// <response code="200">Retorna um token de acesso JWT válido</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(TokenDto))]
        [ProducesResponseType(400, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<string>> Login([FromBody] ClienteDto clienteDto, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteService.FindAsync(clienteDto, cancellationToken);
                if (cliente is null)
                {
                    return Unauthorized(new DefaultResultExceptionDto(StatusCodes.Status401Unauthorized, "Cpf ou senha incorreta!"));
                }
                var token = _tokenProvider.GenerateToken(clienteDto.Cpf);

                var bearerToken = new TokenDto { BearerToken = token };

                return Ok(bearerToken);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }

        }

        /// <summary>
        /// Registra um novo cliente na aplicação.
        /// </summary>
        /// <param name="clienteDto">Objeto contendo informações do novo cliente.</param>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna um token de acesso JWT válido para o cliente recém-registrado.
        /// </returns>
        /// <response code="201">O cliente foi registrado com sucesso e um token de acesso foi gerado.</response>
        /// <response code="409">Conflito. Um cliente com o mesmo CPF já está registrado.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(TokenDto))]
        [ProducesResponseType(400, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(409, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<string>> Register([FromBody] ClienteDto clienteDto, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteService.SaveAsync(clienteDto, cancellationToken);

                var token = _tokenProvider.GenerateToken(cliente.Cpf);
                await _unitOfWork.CommitTransactionAsync();

                var bearerToken = new TokenDto { BearerToken = token };
                return Created(HttpContext.Request.Path, bearerToken);
            }
            catch (AuthClienteExistenteRegisterException ex)
            {
                return Conflict(new DefaultResultExceptionDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }
    }
}
