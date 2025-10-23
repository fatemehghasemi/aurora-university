using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class MarksController(IMarkService markService) : BaseController
{
    private readonly IMarkService _markService = markService;

    [HttpGet("student/{studentId}")]
    public async Task<IEnumerable<MarkModel>> GetByStudentId(Guid studentId)
    {
        return await _markService.GetByStudentIdAsync(studentId);
    }

    [HttpPost]
    public async Task<ActionResult<MarkModel>> RecordMark([FromBody] MarkModel dto)
    {
        var validator = new MarkDtoValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var created = await _markService.RecordMarkAsync(dto);
        return CreatedAtAction(nameof(GetByStudentId), new { studentId = created.StudentId }, created);
    }
}
