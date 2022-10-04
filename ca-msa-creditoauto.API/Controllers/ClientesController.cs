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
       return Ok(await _clInfraestructura.ObtenerClientes());
    }

    [HttpGet]
    public async Task<IActionResult> GetClienteAsync(int id)
    {
        var result = await _clInfraestructura.ObtenerCliente(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);   
    }

    [HttpPost]
    public async Task<IActionResult> AgregarClienteAsync([FromBody] ClienteType cliente)
    {
        if (cliente == null)
            return BadRequest();

        var createdCliente = await _clInfraestructura.AgregarCliente(cliente);
        return Ok(createdCliente);   
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarClienteAsync(int id,[FromBody] ClienteType cliente)
    {
        var clienteAct = await _clInfraestructura.ObtenerCliente(id);
        if (!clienteAct.Succeeded)
            return NotFound();

        return Ok(await _clInfraestructura.ActualizarCliente(id, cliente));
    }


    [HttpDelete]
    public async Task<IActionResult> EliminarClienteAsync(int id)
    {
        var clEliminar = await _clInfraestructura.ObtenerCliente(id);
        if (!clEliminar.Succeeded)
        {
            return NotFound();
        }
        return Ok(await _clInfraestructura.EliminarCliente(id));
    }
}
