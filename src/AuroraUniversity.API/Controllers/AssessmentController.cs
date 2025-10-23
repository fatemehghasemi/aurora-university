using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class AssessmentController(IAssessmentService service) : BaseController
{
    private readonly IAssessmentService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assessments = await _service.GetAllAsync();
        return Ok(assessments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var assessment = await _service.GetByIdAsync(id);
        if (assessment == null) return NotFound();
        return Ok(assessment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssessmentModel model)
    {
        var created = await _service.CreateAssessmentAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
