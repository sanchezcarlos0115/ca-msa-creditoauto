using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IClienteInfraestructura _clInfraestructura;

    public ClientesController(IClienteInfraestructura clInfraestructura)
    {
        _clInfraestructura = clInfraestructura;
    }

    [HttpGet("GetClientesAsync")]
    public async Task<IActionResult> GetClientesAsync()
    {
        try
        {
            return Ok(await _clInfraestructura.ObtenerClientes());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetClienteAsync(int id)
    {
        try
        {
            var result = await _clInfraestructura.ObtenerCliente(id);
            if (!result.Succeeded) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarClienteAsync([FromBody] ClienteType cliente)
    {
        try
        {
            if (cliente == null)
                return BadRequest();

            var createdCliente = await _clInfraestructura.AgregarCliente(cliente);
            return Ok(createdCliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarClienteAsync(int id,[FromBody] ClienteType cliente)
    {
        try
        {
            var clienteAct = await _clInfraestructura.ObtenerCliente(id);
            if (!clienteAct.Succeeded)
                return NotFound();

            return Ok(await _clInfraestructura.ActualizarCliente(id, cliente));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }


    [HttpDelete]
    public async Task<IActionResult> EliminarClienteAsync(int id)
    {
        try
        {
            var clEliminar = await _clInfraestructura.ObtenerCliente(id);
            if (!clEliminar.Succeeded)
            {
                return NotFound();
            }
            return Ok(await _clInfraestructura.EliminarCliente(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

}
