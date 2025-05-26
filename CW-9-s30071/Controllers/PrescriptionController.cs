using CW_9_s30071.Exceptions;
using CW_9_s30071.Models.DTOs;
using CW_9_s30071.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_9_s30071.Controllers;

[ApiController]
[Route("api")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpPost]
    [Route("prescription")]
    public async Task<ActionResult> AddPrescription([FromBody] PrescriptionCreateDTO prescriptionData)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionData);
            return Ok(prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
 }