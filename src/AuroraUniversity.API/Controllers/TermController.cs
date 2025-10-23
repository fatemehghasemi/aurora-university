using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class TermController(ITermService TermService) : BaseController
{
    private readonly ITermService _TermService = TermService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _TermService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _TermService.GetByIdAsync(id);
        return result is null ? NotFound($"Module with id {id} not found.") : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TermModel model)
    {
        var created = await _TermService.CreateAsync(model);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TermModel model)
    {
        var result = await _TermService.UpdateAsync(id, model);
        return !result ? NotFound($"Term with id {id} not found.") : Ok(model);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _TermService.DeleteAsync(id);
        return !deleted ? NotFound($"Term with id {id} not found.") : NoContent();
    }
}
