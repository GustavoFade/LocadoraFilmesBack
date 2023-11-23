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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private readonly ILogger<FilmeController> _logger;
        private readonly IFilmeService _filmeService;
        private readonly IUnitOfWork _unitOfWork;

        public FilmeController(ILogger<FilmeController> logger, IFilmeService filmeService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _filmeService = filmeService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém as informações de um filme pelo seu ID.
        /// </summary>
        /// <param name="idFilme">ID único do filme.</param>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna um objeto FilmeDto que contém as informações detalhadas do filme.
        /// </returns>
        /// <response code="200">Retorna os detalhes do filme.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpGet("{idFilme:int}", Name = nameof(ObterFilme))]
        [ProducesResponseType(200, Type = typeof(FilmeResponseDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<FilmeResponseDto>> ObterFilme([FromRoute] int idFilme, CancellationToken cancellationToken)
        {
            try
            {
                var filme = await _filmeService.FindAsync(idFilme, cancellationToken);
                if (filme is null)
                {
                    return NotFound();
                }
                return Ok(filme);
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
        /// Obtém todos os filmes disponíveis na aplicação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna um array de objetos FilmeDto que contém informações resumidas de todos os filmes.
        /// </returns>
        /// <response code="200">Retorna a lista de todos os filmes.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpGet("get-all-filmes", Name = nameof(ObterTodosFilmes))]
        [ProducesResponseType(200, Type = typeof(FilmeResponseDto[]))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<FilmeResponseDto[]>> ObterTodosFilmes(CancellationToken cancellationToken)
        {
            try
            {
                var filmes = await _filmeService.FindAllAsync(cancellationToken);
                return Ok(filmes);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Cria um novo filme na aplicação.
        /// </summary>
        /// <param name="filmeDto">Objeto contendo informações do novo filme.</param>
        /// <param name="cancellationToken">Token de cancelamento para interromper a operação.</param>
        /// <returns>
        /// Retorna os detalhes do filme recém-criado.
        /// </returns>
        /// <response code="201">O filme foi criado com sucesso.</response>
        /// <response code="400">Requisição inválida. Verifique os dados fornecidos.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPost(Name = nameof(CriarFilme))]
        [ProducesResponseType(200, Type = typeof(FilmeResponseDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult<FilmeResponseDto>> CriarFilme([FromBody] FilmeRequestDto filmeDto, CancellationToken cancellationToken)
        {
            try
            {
                var filme = await _filmeService.SaveAsync(filmeDto, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return Created(HttpContext.Request.Path, filme);
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Altera as informações de um filme existente na aplicação.
        /// </summary>
        /// <param name="idFilme">ID único do filme a ser alterado.</param>
        /// <param name="filmeDto">Objeto contendo as novas informações do filme.</param>
        /// <returns>
        /// Retorna uma resposta sem conteúdo para indicar que o filme foi alterado com sucesso.
        /// </returns>
        /// <response code="204">O filme foi alterado com sucesso.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpPut("{idFilme:int}", Name = nameof(AlterarFilme))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(400, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult> AlterarFilme([FromRoute] int idFilme, [FromBody] FilmeRequestDto filmeDto, CancellationToken cancellationToken)
        {
            try
            {
                await _filmeService.UpdateAsync(idFilme, filmeDto, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return NoContent();
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new DefaultResultExceptionDto(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Exclui um filme da aplicação pelo seu ID.
        /// </summary>
        /// <param name="idFilme">ID único do filme a ser excluído.</param>
        /// <returns>
        /// Retorna uma resposta sem conteúdo para indicar que o filme foi excluído com sucesso.
        /// </returns>
        /// <response code="204">O filme foi excluído com sucesso.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpDelete("{idFilme:int}", Name = nameof(DeletarFilme))]
        [ProducesResponseType(400, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult> DeletarFilme([FromRoute] int idFilme, CancellationToken cancellationToken)
        {
            try
            {
                await _filmeService.DeleteAsync(idFilme, cancellationToken);
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
        /// Exclui vários filmes da aplicação pelos seus IDs.
        /// </summary>
        /// <param name="idFilme">Array contendo os IDs únicos dos filmes a serem excluídos.</param>
        /// <returns>
        /// Retorna uma resposta sem conteúdo para indicar que os filmes foram excluídos com sucesso.
        /// </returns>
        /// <response code="204">Os filmes foram excluídos com sucesso.</response>
        /// <response code="401">Não autorizado. O token de acesso pode ser inválido.</response>
        /// <response code="500">Ocorreu um erro. Tente novamente mais tarde!</response>
        [HttpDelete("filmes", Name = nameof(DeletarVariosFilmes))]
        [ProducesResponseType(400, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(401, Type = typeof(DefaultResultExceptionDto))]
        [ProducesResponseType(500, Type = typeof(DefaultResultExceptionDto))]
        public async Task<ActionResult> DeletarVariosFilmes([FromQuery, Required, MinLength(1)] List<int> idFilme, CancellationToken cancellationToken)
        {
            try
            {
                await _filmeService.DeleteManyAsync(idFilme, cancellationToken);
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
