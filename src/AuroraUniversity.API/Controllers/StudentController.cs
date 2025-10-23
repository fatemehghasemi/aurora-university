using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class StudentsController(IStudentService studentService) : BaseController
{
    private readonly IStudentService _studentService = studentService;

    [HttpGet]
    public async Task<IEnumerable<StudentModel>> GetAll()
    {
        var result = await _studentService.GetAllAsync();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentModel>> GetById(Guid id)
    {
        var result = await _studentService.GetByIdAsync(id);
        return result is null ? NotFound($"Student with id {id} not found.") : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StudentModel>> Create(StudentModel dto)
    {
        var created = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, StudentModel dto)
    {
        if (id != dto.Id) return BadRequest();
        var result = await _studentService.UpdateAsync(dto);
        return !result ? NotFound($"Module with id {id} not found.") : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
