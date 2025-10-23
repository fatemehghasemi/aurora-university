using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class EnrollmentController(IEnrollmentService service) : BaseController
{
    private readonly IEnrollmentService _service = service;

    [HttpPost]
    public async Task<IActionResult> EnrollStudent([FromBody] EnrollmentModel model)
    {
        try
        {
            var enrollment = await _service.BatchEnrollAndAllocateAsync(model.ModuleId, model.StudentIds);
            return Ok(enrollment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
