using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class GradeController(IStudentRepository studentRepository, IGradeCalculationService gradeService) : BaseController
{
    private readonly IStudentRepository _studentRepository = studentRepository;
    private readonly IGradeCalculationService _gradeService = gradeService;


    [HttpGet("{studentId}/grades")]
    public async Task<IActionResult> GetStudentGrades(Guid studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null) return NotFound($"Student with ID {studentId} not found.");

        var grades = _gradeService.CalculateStudentGrades(student);
        return Ok(grades);
    }
}
