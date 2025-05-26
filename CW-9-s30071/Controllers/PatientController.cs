using CW_9_s30071.Exceptions;
using CW_9_s30071.Services;
using Microsoft.AspNetCore.Mvc;
namespace CW_9_s30071.Controllers;

[ApiController]
[Route("api")]
public class PatientController(IDbService service) : ControllerBase
{
    [HttpGet]
    [Route("patient/{id}")]
    public async Task<ActionResult> GetPatientDetails([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientDetailsByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}