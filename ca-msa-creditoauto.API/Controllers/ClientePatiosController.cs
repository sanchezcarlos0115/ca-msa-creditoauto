using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class ClientePatiosController : ControllerBase
{
    private readonly IClientePatioInfraestructura _clInfraestructura;

    public ClientePatiosController(IClientePatioInfraestructura clInfraestructura)
    {
        _clInfraestructura = clInfraestructura;
    }

    [HttpGet("GetClientePatiosAsync")]
    public async Task<IActionResult> GetClientePatiosAsync()
    {
       return Ok(await _clInfraestructura.ObtenerClientePatios()); 
    }

    [HttpGet]
    public async Task<IActionResult> GetClientePatioAsync(int id)
    {
        var result = await _clInfraestructura.ObtenerClientePatio(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AgregarClientePatioAsync([FromBody] ClientePatioType obj)
    {
        if (obj == null)
            return BadRequest();

        var createdCliente = await _clInfraestructura.AgregarClientePatio(obj);
        return Ok(createdCliente);
    }

    [HttpPut]
    public async Task<ActionResult> ActualizarClientePatioAsync(int id,[FromBody] ClientePatioType obj)
    {
        var clienteAct = await _clInfraestructura.ObtenerClientePatio(id);
        if (!clienteAct.Succeeded)
            return NotFound();

        return Ok(await _clInfraestructura.ActualizarClientePatio(id, obj));  
    }


    [HttpDelete]
    public async Task<ActionResult> EliminarClientePatioAsync(int id)
    {
        var clEliminar = await _clInfraestructura.ObtenerClientePatio(id);
        if (!clEliminar.Succeeded)
        {
            return NotFound();
        }
        return Ok(await _clInfraestructura.EliminarClientePatio(id));
    }

}
