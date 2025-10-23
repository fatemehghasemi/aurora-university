using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class TermModuleController(ITermModuleService termModuleService) : BaseController
{
    private readonly ITermModuleService _termModuleService = termModuleService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _termModuleService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _termModuleService.GetByIdAsync(id);
        return result is null ? NotFound($"Module with id {id} not found.") : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TermModuleModel model)
    {
        var created = await _termModuleService.CreateAsync(model);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TermModuleModel model)
    {
        var result = await _termModuleService.UpdateAsync(id, model);
        return !result ? NotFound($"Module with id {id} not found.") : Ok(model);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _termModuleService.DeleteAsync(id);
        return !deleted ? NotFound($"Module with id {id} not found.") : NoContent();
    }
}
