using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class PatioAutosController : ControllerBase
{
    private readonly IPatioAutoInfraestructura _infraestructura;

    public PatioAutosController(IPatioAutoInfraestructura infraestructura)
    {
        _infraestructura = infraestructura;
    }

    [HttpGet("GetPatioAutosAsync")]
    public async Task<IActionResult> GetPatioAutosAsync()
    {
        try
        {
            return Ok(await _infraestructura.ObtenerPatioAutos());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPatioAutoAsync(int id)
    {
        try
        {
            var result = await _infraestructura.ObtenerPatioAuto(id);
            if (!result.Succeeded) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarPatioAutoAsync([FromBody] PatioAutoType patio)
    {
        try
        {
            if (patio == null)
                return BadRequest();
            return Ok(await _infraestructura.AgregarPatioAuto(patio));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarPatioAutoAsync(int id,[FromBody] PatioAutoType patio)
    {
        try
        {
            var patioAct = await _infraestructura.ObtenerPatioAuto(id);
            if (!patioAct.Succeeded)
                return NotFound();

            return Ok(await _infraestructura.ActualizarPatioAuto(id, patio));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }


    [HttpDelete]
    public async Task<IActionResult> EliminarPatioAutoAsync(int id)
    {
        try
        {
            var objEliminar = await _infraestructura.ObtenerPatioAuto(id);
            if (!objEliminar.Succeeded)
            {
                return NotFound();
            }
            return Ok(await _infraestructura.EliminarPatioAuto(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
        }
    }

}
