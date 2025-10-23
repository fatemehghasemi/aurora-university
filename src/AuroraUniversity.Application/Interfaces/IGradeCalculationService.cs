using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Application.Interfaces;

public interface IGradeCalculationService
{
    StudentGradesModel CalculateStudentGrades(Student student);
}