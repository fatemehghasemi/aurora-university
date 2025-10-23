using AuroraUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.SeedData;

public static class Seed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        // --- STAFF ---
        var staffs = new[]
        {
            new Staff { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Smith", Email = "alice@aurora.edu" },
            new Staff { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Johnson", Email = "bob@aurora.edu" },
            new Staff { Id = Guid.NewGuid(), FirstName = "Carol", LastName = "Davis", Email = "carol@aurora.edu" }
        };
        modelBuilder.Entity<Staff>().HasData(staffs);


        // --- STUDENTS ---
        var students = new[]
        {
            new Student { Id = Guid.NewGuid(), FirstName = "Olivia", LastName = "Chen" },
            new Student { Id = Guid.NewGuid(), FirstName = "Liam", LastName = "Johnson" },
            new Student { Id = Guid.NewGuid(), FirstName = "Emma", LastName = "Schmidt" },
            new Student { Id = Guid.NewGuid(), FirstName = "Noah", LastName = "Silva" },
            new Student { Id = Guid.NewGuid(), FirstName = "Ava", LastName = "Dubois" },
            new Student { Id = Guid.NewGuid(), FirstName = "Isabelle", LastName = "Kim" },
            new Student { Id = Guid.NewGuid(), FirstName = "Lucas", LastName = "Garcia" },
            new Student { Id = Guid.NewGuid(), FirstName = "Mia", LastName = "Rossi" },
            new Student { Id = Guid.NewGuid(), FirstName = "Elijah", LastName = "Patel" },
            new Student { Id = Guid.NewGuid(), FirstName = "Sophia", LastName = "Müller" }
        };
        modelBuilder.Entity<Student>().HasData(students);


        // --- Terms ---
        var termId = Guid.NewGuid();
        var startTerm = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        var endTerm = new DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var terms = new[]
        {
            new Term { Id = termId, Code = "TRM1", Title = "Term 1", StartTime = startTerm, EndTime=  endTerm }
        };
        modelBuilder.Entity<Term>().HasData(terms);


        // --- MODULES ---
        var modules = new[]
        {
            new TermModule { Id = Guid.NewGuid(), TermId = termId, Credits = 4, Code = "MATH101", Title = "Mathematics I", Capacity = 30,  StaffId = staffs[0].Id },
            new TermModule { Id = Guid.NewGuid(), TermId = termId, Credits = 3, Code = "PHYS102", Title = "Physics I", Capacity = 25,  StaffId = staffs[1].Id },
            new TermModule { Id = Guid.NewGuid(), TermId = termId, Credits = 3, Code = "CS103", Title = "Intro to Programming", Capacity = 35,  StaffId = staffs[2].Id },
            new TermModule { Id = Guid.NewGuid(), TermId = termId, Credits = 2, Code = "STAT104", Title = "Statistics", Capacity = 20,  StaffId = staffs[0].Id }
        };
        modelBuilder.Entity<TermModule>().HasData(modules);


        // --- ASSESSMENTS ---
        var assessments = new[]
        {
            // ----------------------------------------------------------------------------------
            // Module 0: Math (Total Weight: 1.0m)
            // ----------------------------------------------------------------------------------
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[0].Id, Title = "Weekly Quizzes", Date = DateTime.UtcNow.AddDays(7), Weight = 0.2m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[0].Id, Title = "Math Midterm", Date = DateTime.UtcNow.AddDays(25), Weight = 0.3m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[0].Id, Title = "Math Final Exam", Date = DateTime.UtcNow.AddDays(55), Weight = 0.5m },
    
            // ----------------------------------------------------------------------------------
            // Module 1: Physics (Total Weight: 1.0m)
            // ----------------------------------------------------------------------------------
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[1].Id, Title = "Lab Report 1", Date = DateTime.UtcNow.AddDays(15), Weight = 0.2m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[1].Id, Title = "Problem Set Submission", Date = DateTime.UtcNow.AddDays(30), Weight = 0.2m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[1].Id, Title = "Physics Final Exam", Date = DateTime.UtcNow.AddDays(60), Weight = 0.6m },
    
            // ----------------------------------------------------------------------------------
            // Module 2: Programming (Total Weight: 1.0m)
            // ----------------------------------------------------------------------------------
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[2].Id, Title = "Programming Project Phase 1", Date = DateTime.UtcNow.AddDays(25), Weight = 0.35m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[2].Id, Title = "Mid-Term Code Review", Date = DateTime.UtcNow.AddDays(40), Weight = 0.15m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[2].Id, Title = "Final Programming Project", Date = DateTime.UtcNow.AddDays(75), Weight = 0.5m },

            // ----------------------------------------------------------------------------------
            // Module 3: Statistics (Total Weight: 1.0m)
            // ----------------------------------------------------------------------------------
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[3].Id, Title = "Group Presentation", Date = DateTime.UtcNow.AddDays(20), Weight = 0.3m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[3].Id, Title = "Data Analysis Report", Date = DateTime.UtcNow.AddDays(45), Weight = 0.4m },
            new Assessment { Id = Guid.NewGuid(), ModuleId = modules[3].Id, Title = "Statistics Final Exam", Date = DateTime.UtcNow.AddDays(70), Weight = 0.3m }
        };
        modelBuilder.Entity<Assessment>().HasData(assessments);

        // --- SESSIONS ---
        // Create 2 sessions (1 Lecture, 1 Seminar) for each of the 4 modules.
        // Dates are relative to the term start (Sept 1, 2025) and current date (Oct 23, 2025).

        // Define the start date of Term 1 for reference (Sep 1, 2025 00:00:00 UTC)
        var termStart = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        var sessions = new List<Session>();

        // M0: MATH101 (Taught by Staffs[0] - Alice Smith)
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[0].Id,
            StaffId = staffs[0].Id,
            StartTime = termStart.AddDays(2).AddHours(10),
            EndTime = termStart.AddDays(2).AddHours(12), // Sep 3, 10:00-12:00
            Location = "Lecture Hall A"
        });
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[0].Id,
            StaffId = staffs[0].Id,
            StartTime = termStart.AddDays(3).AddHours(14),
            EndTime = termStart.AddDays(3).AddHours(16), // Sep 4, 14:00-16:00
            Location = "Seminar Room 101"
        });

        // M1: PHYS102 (Taught by Staffs[1] - Bob Johnson)
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[1].Id,
            StaffId = staffs[1].Id,
            StartTime = termStart.AddDays(4).AddHours(9),
            EndTime = termStart.AddDays(4).AddHours(11), // Sep 5, 09:00-11:00
            Location = "Lecture Hall B"
        });
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[1].Id,
            StaffId = staffs[1].Id,
            StartTime = termStart.AddDays(5).AddHours(13),
            EndTime = termStart.AddDays(5).AddHours(15), // Sep 6, 13:00-15:00
            Location = "Lab 205"
        });

        // M2: CS103 (Taught by Staffs[2] - Carol Davis)
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[2].Id,
            StaffId = staffs[2].Id,
            StartTime = termStart.AddDays(7).AddHours(11),
            EndTime = termStart.AddDays(7).AddHours(13), // Sep 8, 11:00-13:00
            Location = "Virtual Classroom"
        });
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[2].Id,
            StaffId = staffs[2].Id,
            StartTime = termStart.AddDays(8).AddHours(16),
            EndTime = termStart.AddDays(8).AddHours(18), // Sep 9, 16:00-18:00
            Location = "Lab 103"
        });

        // M3: STAT104 (Taught by Staffs[0] - Alice Smith)
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[3].Id,
            StaffId = staffs[0].Id,
            StartTime = termStart.AddDays(9).AddHours(10),
            EndTime = termStart.AddDays(9).AddHours(12), // Sep 10, 10:00-12:00
            Location = "Lecture Hall C"
        });
        sessions.Add(new Session
        {
            Id = Guid.NewGuid(),
            ModuleId = modules[3].Id,
            StaffId = staffs[0].Id,
            StartTime = termStart.AddDays(10).AddHours(13),
            EndTime = termStart.AddDays(10).AddHours(15), // Sep 11, 13:00-15:00
            Location = "Seminar Room 202"
        });

        modelBuilder.Entity<Session>().HasData(sessions);

        // --- STUDENT-MODULE ENROLLMENT (Many-to-Many setup) ---
        // Enrollment: All 10 students enroll in MATH101, PHYS102, and CS103.
        // The first 5 students also enroll in STAT104.

        var studentModuleEnrollments = new List<object>();

        // Enroll all 10 students in the first 3 modules
        for (int i = 0; i < 10; i++)
        {
            // Math, Physics, Programming
            studentModuleEnrollments.Add(new { EnrolledModulesId = modules[0].Id, EnrolledStudentsId = students[i].Id });
            studentModuleEnrollments.Add(new { EnrolledModulesId = modules[1].Id, EnrolledStudentsId = students[i].Id });
            studentModuleEnrollments.Add(new { EnrolledModulesId = modules[2].Id, EnrolledStudentsId = students[i].Id });

            // Enroll the first 5 students in Statistics
            if (i < 5)
            {
                studentModuleEnrollments.Add(new { EnrolledModulesId = modules[3].Id, EnrolledStudentsId = students[i].Id });
            }
        }

        // Map the Many-to-Many relationship manually (TermModule_EnrolledStudents)
        modelBuilder.Entity<TermModule>()
            .HasMany(m => m.EnrolledStudents)
            .WithMany(s => s.EnrolledModules)
            .UsingEntity(j => j.HasData(studentModuleEnrollments));


        // --- STUDENT-SESSION REGISTRATION (Many-to-Many setup) ---
        // Registration: All enrolled students register for the first session (Lecture) of their respective modules.

        var studentSessionRegistrations = new List<object>();

        // All 10 students register for Math Lecture (sessions[0])
        for (int i = 0; i < 10; i++)
        {
            studentSessionRegistrations.Add(new { RegisteredSessionsId = sessions[0].Id, RegisteredStudentsId = students[i].Id });
        }

        // All 10 students register for Physics Lecture (sessions[2])
        for (int i = 0; i < 10; i++)
        {
            studentSessionRegistrations.Add(new { RegisteredSessionsId = sessions[2].Id, RegisteredStudentsId = students[i].Id });
        }

        // All 10 students register for CS Lecture (sessions[4])
        for (int i = 0; i < 10; i++)
        {
            studentSessionRegistrations.Add(new { RegisteredSessionsId = sessions[4].Id, RegisteredStudentsId = students[i].Id });
        }

        // First 5 students register for Statistics Lecture (sessions[6])
        for (int i = 0; i < 5; i++)
        {
            studentSessionRegistrations.Add(new { RegisteredSessionsId = sessions[6].Id, RegisteredStudentsId = students[i].Id });
        }

        // Map the Many-to-Many relationship manually (Session_RegisteredStudents)
        modelBuilder.Entity<Session>()
            .HasMany(s => s.RegisteredStudents)
            .WithMany(s => s.RegisteredSessions)
            .UsingEntity(j => j.HasData(studentSessionRegistrations));


        // --- MARKS ---
        // Give marks to the students. We need to match Students to Assessments.
        // We'll use the first 5 students for a variety of scenarios.

        var marks = new List<Mark>();
        var allAssessments = assessments.ToArray();
        var staffAliceId = staffs[0].Id; // Alice Smith
        var staffBobId = staffs[1].Id;   // Bob Johnson

        // Helper to find an assessment by its title (since the ID is a new GUID each time)
        Guid GetAssessmentId(string title) => allAssessments.First(a => a.Title == title).Id;

        // ----------------------------------------------------------------------------------
        // Student 0: Olivia Chen (Good Student)
        // ----------------------------------------------------------------------------------
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[0].Id, AssessmentId = GetAssessmentId("Weekly Quizzes"), Score = 85m, DateRecorded = DateTime.UtcNow.AddDays(5), RecordedByStaffId = staffAliceId });
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[0].Id, AssessmentId = GetAssessmentId("Math Midterm"), Score = 92m, DateRecorded = DateTime.UtcNow.AddDays(26), RecordedByStaffId = staffAliceId });
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[0].Id, AssessmentId = GetAssessmentId("Lab Report 1"), Score = 78m, DateRecorded = DateTime.UtcNow.AddDays(16), RecordedByStaffId = staffBobId });
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[0].Id, AssessmentId = GetAssessmentId("Programming Project Phase 1"), Score = 95m, DateRecorded = DateTime.UtcNow.AddDays(26) });

        // ----------------------------------------------------------------------------------
        // Student 1: Liam Johnson (Needs a Resit)
        // ----------------------------------------------------------------------------------
        // 1a. Math Midterm - FAIL (requires resit)
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[1].Id, AssessmentId = GetAssessmentId("Math Midterm"), Score = 35m, DateRecorded = DateTime.UtcNow.AddDays(26), RecordedByStaffId = staffAliceId });

        // 1b. Math Midterm - RESIT (Passes the resit)
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[1].Id, AssessmentId = GetAssessmentId("Math Midterm"), Score = 65m, IsResit = true, DateRecorded = DateTime.UtcNow.AddDays(40), RecordedByStaffId = staffAliceId, Comments = "Resit passed." });

        // 1c. Physics Problem Set - Pass
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[1].Id, AssessmentId = GetAssessmentId("Problem Set Submission"), Score = 70m, DateRecorded = DateTime.UtcNow.AddDays(31), RecordedByStaffId = staffBobId });

        // ----------------------------------------------------------------------------------
        // Student 2: Emma Schmidt (High Score)
        // ----------------------------------------------------------------------------------
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[2].Id, AssessmentId = GetAssessmentId("Math Midterm"), Score = 98m, DateRecorded = DateTime.UtcNow.AddDays(26), RecordedByStaffId = staffAliceId });
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[2].Id, AssessmentId = GetAssessmentId("Group Presentation"), Score = 88m, DateRecorded = DateTime.UtcNow.AddDays(21), RecordedByStaffId = staffAliceId });

        // ----------------------------------------------------------------------------------
        // Student 3: Noah Silva (Failed Resit)
        // ----------------------------------------------------------------------------------
        // 3a. Programming Project Phase 1 - FAIL
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[3].Id, AssessmentId = GetAssessmentId("Programming Project Phase 1"), Score = 30m, DateRecorded = DateTime.UtcNow.AddDays(26) });

        // 3b. Programming Project Phase 1 - RESIT (Still fails, or cap mark at pass)
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[3].Id, AssessmentId = GetAssessmentId("Programming Project Phase 1"), Score = 38m, IsResit = true, DateRecorded = DateTime.UtcNow.AddDays(50), Comments = "Resit failed to meet passing threshold." });

        // ----------------------------------------------------------------------------------
        // Student 4: Ava Dubois (Complete Data for Math)
        // ----------------------------------------------------------------------------------
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[4].Id, AssessmentId = GetAssessmentId("Weekly Quizzes"), Score = 70m, DateRecorded = DateTime.UtcNow.AddDays(5), RecordedByStaffId = staffAliceId });
        marks.Add(new Mark { Id = Guid.NewGuid(), StudentId = students[4].Id, AssessmentId = GetAssessmentId("Math Midterm"), Score = 75m, DateRecorded = DateTime.UtcNow.AddDays(26), RecordedByStaffId = staffAliceId });

        modelBuilder.Entity<Mark>().HasData(marks);
    }
}
