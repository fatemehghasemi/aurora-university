using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Application.Services;

public class GradeCalculationService : IGradeCalculationService
{
    public StudentGradesModel CalculateStudentGrades(Student student)
    {
        // 1. Calculate Final Mark for Each Module
        var moduleResults = student.Marks
            // Group 1: Group by Assessment (to find the best mark for resits)
            .GroupBy(m => m.AssessmentId)
            .Select(assessmentGroup =>
            {
                var bestMark = assessmentGroup.Max(m => m.Score);
                var assessment = assessmentGroup.First().Assessment!;

                return new
                {
                    ModuleId = assessment.ModuleId,
                    ModuleName = assessment.Module!.Title,
                    Credits = assessment.Module.Credits, // Needed for final credit-weighted average

                    // Weighted Score = Best Mark * Assessment Weight
                    WeightedScoreContribution = bestMark * assessment.Weight,
                    AssessmentDate = assessment.Date
                };
            })
            // Group 2: Group by Module (to calculate the module's final mark)
            .GroupBy(x => x.ModuleId)
            .Select(moduleGroup =>
            {
                // Module Mark = Sum of (Best Mark * Assessment Weight)
                var finalMark = moduleGroup.Sum(x => x.WeightedScoreContribution);

                return new ModuleGradeModel
                {
                    ModuleId = moduleGroup.Key,
                    ModuleName = moduleGroup.First().ModuleName,
                    Credits = moduleGroup.First().Credits,
                    FinalMark = finalMark
                };
            })
            .ToList();

        // 2. Calculate the Term Average (Credit-Weighted Average)

        // Sum of (Module Mark * Module Credits)
        var totalWeightedScore = moduleResults.Sum(m => m.FinalMark * m.Credits);

        // Sum of total Credits
        var totalCredits = moduleResults.Sum(m => m.Credits);

        // Term Average (Credit-Weighted Average)
        var termAverage = totalCredits > 0
            ? totalWeightedScore / totalCredits
            : 0.0m;

        // 3. Apply Rounding (Sensible rounding rule: Round to one decimal place)
        var finalRoundedAverage = Math.Round(termAverage, 1, MidpointRounding.AwayFromZero);

        // 4. Determine Date Range
        var allAssessmentDates = student.Marks.Select(m => m.Assessment!.Date).ToList();
        var minDate = allAssessmentDates.Any() ? allAssessmentDates.Min() : DateTime.MinValue;
        var maxDate = allAssessmentDates.Any() ? allAssessmentDates.Max() : DateTime.MinValue;

        // 5. Progression Decision
        var progression = finalRoundedAverage >= 40.0m ? "Pass" : "Refer";

        // 6. Return Final Model
        return new StudentGradesModel
        {
            StudentId = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            ModuleGrades = moduleResults,

            // Final Results
            TermAverage = finalRoundedAverage,
            AssessmentStart = minDate.ToUniversalTime(),
            AssessmentEnd = maxDate.ToUniversalTime(),
            ProgressionDecision = progression
        };
    }

}
