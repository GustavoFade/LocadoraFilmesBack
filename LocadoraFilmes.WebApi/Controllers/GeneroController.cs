using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.DTOs.ExceptionDTO;
using LocadoraFilmes.Application.DTOs.Request;
using LocadoraFilmes.Application.DTOs.Response;
using LocadoraFilmes.Application.Exceptions;
using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly ILogger<GeneroController> _logger;
        private readonly IGeneroService _generoService;
        private readonly IUnitOfWork _unitOfWork;

        public GeneroController(ILogger<GeneroController> logger, IGeneroService generoService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _generoService = generoService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém as informações de um genero pelo seu ID.
        /// </summary>
        /// <param name="idGenero">ID único do genero.</param>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna um objeto GeneroDto que contém as informações detalhadas do genero.
        /// </returns>
        /// <response code="200">Retorna os detalhes do genero.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpGet("{idGenero:int}", Name = nameof(ObterGenero))]
        [ProducesResponseType(200, Type = typeof(GeneroResponseDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<GeneroResponseDto>> ObterGenero([FromRoute] int idGenero, CancellationToken cancellationToken)
        {
            try
            {
                var genero = await _generoService.FindAsync(idGenero, cancellationToken);
                if (genero is null)
                {
                    return NotFound();
                }
                return Ok(genero);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new DefaultResultExceptionDto(StatusCodes.Status404NotFound, ex.Message));
            }
        }


        /// <summary>
        /// Obtém todos os generos disponíveis na aplicação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna um array de objetos GeneroDto que contém informações resumidas de todos os generos.
        /// </returns>
        /// <response code="200">Retorna a lista de todos os generos.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpGet("get-all-generos", Name = nameof(ObterTodosGeneros))]
        [ProducesResponseType(200, Type = typeof(GeneroRequestDto[]))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<GeneroResponseDto[]>> ObterTodosGeneros(CancellationToken cancellationToken)
        {
            try
            {
                var generos = await _generoService.FindAllAsync(cancellationToken);
                return Ok(generos);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Cria um novo genero na aplicação.
        /// </summary>
        /// <param name="generoDto">Objeto contendo informações do novo genero.</param>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna os detalhes do genero recém-criado.
        /// </returns>
        /// <response code="201">O genero foi criado com sucesso.</response>
        /// <response code="400">Requisição inválida. Verifique os dados fornecidos.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPost(Name = nameof(CriarGenero))]
        [ProducesResponseType(200, Type = typeof(GeneroResponseDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<GeneroResponseDto>> CriarGenero([FromBody] GeneroRequestDto generoDto, CancellationToken cancellationToken)
        {
            try
            {
                var genero = await _generoService.SaveAsync(generoDto, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return Created(HttpContext.Request.Path, genero);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Altera as informações de um genero existente na aplicação.
        /// </summary>
        /// <param name="idGenero">ID único do genero a ser alterado.</param>
        /// <param name="generoDto">Objeto contendo as novas informações do genero.</param>
        /// <returns>
        /// Retorna uma resposta sem conteúdo para indicar que o genero foi alterado com sucesso.
        /// </returns>
        /// <response code="204">O genero foi alterado com sucesso.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPut("{idGenero:int}", Name = nameof(AlterarGenero))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult> AlterarGenero([FromRoute] int idGenero, [FromBody] GeneroRequestDto generoDto)
        {
            try
            {
                await _generoService.UpdateAsync(idGenero, generoDto);
                await _unitOfWork.CommitTransactionAsync();

                return NoContent();
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new DefaultResultExceptionDto(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        /// <summary>
        /// Exclui um genero da aplicação pelo seu ID.
        /// </summary>
        /// <param name="idGenero">ID único do genero a ser excluído.</param>
        /// <returns>
        /// Retorna uma resposta sem conteúdo para indicar que o genero foi excluído com sucesso.
        /// </returns>
        /// <response code="204">O genero foi excluído com sucesso.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpDelete("{idGenero:int}", Name = nameof(DeletarGenero))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult> DeletarGenero([FromRoute] int idGenero, CancellationToken cancellationToken)
        {
            try
            {
                await _generoService.DeleteAsync(idGenero, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return NoContent();
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new DefaultResultExceptionDto(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}
