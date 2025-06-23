using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using CadastroDeClienteMuralis.Dominio.Exceptions;
using CadastroDeClienteMuralis.Dominio.Mediador.Commands;
using CadastroDeClienteMuralis.Dominio.Mediador.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCliente.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClienteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastrar um cliente
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarClienteAsync(
        [FromBody] CreateClienteRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CriarClienteCommand(request);
            var resultado = await _mediator.Send(command, cancellationToken);
            return CreatedAtRoute("ObterClientePorId", new { id = resultado.Id }, resultado);
        }
        catch (ClienteException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
        }
    }


    /// <summary>
    /// Obter cliente por ID
    /// </summary>
    [HttpGet("{id}", Name = "ObterClientePorId")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new ObterClientePorIdQuery(id);
        var resultado = await _mediator.Send(query, cancellationToken);

        return resultado is null
            ? NotFound("Cliente não encontrado.")
            : Ok(resultado);
    }

    /// <summary>
    /// Listar todos os clientes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClienteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTodosAsync(CancellationToken cancellationToken)
    {
        var query = new ListarClientesQuery();
        var resultado = await _mediator.Send(query, cancellationToken);
        return Ok(resultado);
    }

    /// <summary>
    /// Atualizar um cliente
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarClienteAsync(
        [FromBody] UpdateClienteRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new AtualizarClienteCommand(request);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { mensagem = "Cliente atualizado com sucesso!" });
        }
        catch (ClienteException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
        }
    }

    /// <summary>
    /// Deletar um cliente
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletarClienteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeletarClienteCommand(id);
            await _mediator.Send(command, cancellationToken);
            return Ok(new { mensagem = "Cliente deletado com sucesso!" });
        }
        catch (ClienteException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
        }
    }

}
