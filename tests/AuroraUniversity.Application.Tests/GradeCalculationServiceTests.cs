using AuroraUniversity.Application.Services;
using AuroraUniversity.Domain.Entities;

public class GradeCalculationServiceTests
{
    [Fact]
    public void CalculateStudentGrades_ShouldReturnCorrectGrades()
    {
        var module1 = new TermModule { Id = Guid.NewGuid(), Title = "Math", Credits = 10 };
        var module2 = new TermModule { Id = Guid.NewGuid(), Title = "Physics", Credits = 20 };

        var assessment1 = new Assessment
        {
            Id = Guid.NewGuid(),
            Module = module1,
            ModuleId = module1.Id,
            Date = new DateTime(2025, 6, 1),
            Weight = 0.5m
        };
        var assessment2 = new Assessment
        {
            Id = Guid.NewGuid(),
            Module = module1,
            ModuleId = module1.Id,
            Date = new DateTime(2025, 6, 15),
            Weight = 0.5m
        };
        var assessment3 = new Assessment
        {
            Id = Guid.NewGuid(),
            Module = module2,
            ModuleId = module2.Id,
            Date = new DateTime(2025, 6, 10),
            Weight = 1.0m
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "fatemeh",
            LastName = "ghasemi",
            Marks = new List<Mark>
            {
                new Mark { Assessment = assessment1, AssessmentId = assessment1.Id, Score = 60 },
                new Mark { Assessment = assessment2, AssessmentId = assessment2.Id, Score = 80 },
                new Mark { Assessment = assessment3, AssessmentId = assessment3.Id, Score = 70 }
            }
        };

        var service = new GradeCalculationService();

        var result = service.CalculateStudentGrades(student);

        Assert.Equal(student.Id, result.StudentId);
        Assert.Equal("fatemeh", result.FirstName);
        Assert.Equal("ghasemi", result.LastName);

        Assert.Equal(2, result.ModuleGrades.Count);

        var mathGrade = result.ModuleGrades.Find(m => m.ModuleId == module1.Id);
        var physicsGrade = result.ModuleGrades.Find(m => m.ModuleId == module2.Id);

        Assert.NotNull(mathGrade);
        Assert.NotNull(physicsGrade);

        Assert.Equal(70, mathGrade.FinalMark);

        Assert.Equal(70, physicsGrade.FinalMark);

        Assert.Equal(70.0m, result.TermAverage);

        Assert.Equal(assessment1.Date.ToUniversalTime(), result.AssessmentStart);
        Assert.Equal(assessment2.Date.ToUniversalTime(), result.AssessmentEnd);

        Assert.Equal("Pass", result.ProgressionDecision);
    }

    [Fact]
    public void CalculateStudentGrades_ShouldReferIfAverageBelow40()
    {
        var module = new TermModule { Id = Guid.NewGuid(), Title = "Physics", Credits = 10 };
        var assessment = new Assessment
        {
            Id = Guid.NewGuid(),
            Module = module,
            ModuleId = module.Id,
            Date = new DateTime(2025, 6, 1),
            Weight = 1.0m
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "narges",
            LastName = "mahdavi",
            Marks = new List<Mark>
            {
                new Mark { Assessment = assessment, AssessmentId = assessment.Id, Score = 30 }
            }
        };

        var service = new GradeCalculationService();

        var result = service.CalculateStudentGrades(student);

        Assert.Equal("Refer", result.ProgressionDecision);
        Assert.Equal(30.0m, result.TermAverage);
    }

    [Fact]
    public void CalculateStudentGrades_ShouldUseBestMarkForResit()
    {
        var module = new TermModule { Id = Guid.NewGuid(), Title = "Math", Credits = 10 };
        var assessment = new Assessment
        {
            Id = Guid.NewGuid(),
            Module = module,
            ModuleId = module.Id,
            Weight = 1.0m,
            Date = new DateTime(2025, 6, 1)
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "fatemeh",
            LastName = "ghasemi",
            Marks = new List<Mark>
            {
                new Mark { Assessment = assessment, AssessmentId = assessment.Id, Score = 40 },
                new Mark { Assessment = assessment, AssessmentId = assessment.Id, Score = 75 }
            }
        };

        var service = new GradeCalculationService();

        var result = service.CalculateStudentGrades(student);

        Assert.Single(result.ModuleGrades);
        var moduleGrade = result.ModuleGrades[0];

        // نمره باید نمره بالاتر (resit) باشد
        Assert.Equal(75, moduleGrade.FinalMark);

        // میانگین ترم هم باید همان 75 باشد
        Assert.Equal(75.0m, result.TermAverage);

        // progression باید Pass باشد
        Assert.Equal("Pass", result.ProgressionDecision);
    }
}
