using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuroraUniversity.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    TermId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modules_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTermModule",
                columns: table => new
                {
                    EnrolledModulesId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnrolledStudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTermModule", x => new { x.EnrolledModulesId, x.EnrolledStudentsId });
                    table.ForeignKey(
                        name: "FK_StudentTermModule_Modules_EnrolledModulesId",
                        column: x => x.EnrolledModulesId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTermModule_Students_EnrolledStudentsId",
                        column: x => x.EnrolledStudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    IsResit = table.Column<bool>(type: "boolean", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    RecordedByStaffId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marks_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_Staffs_RecordedByStaffId",
                        column: x => x.RecordedByStaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Marks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionStudent",
                columns: table => new
                {
                    RegisteredSessionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegisteredStudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionStudent", x => new { x.RegisteredSessionsId, x.RegisteredStudentsId });
                    table.ForeignKey(
                        name: "FK_SessionStudent_Sessions_RegisteredSessionsId",
                        column: x => x.RegisteredSessionsId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionStudent_Students_RegisteredStudentsId",
                        column: x => x.RegisteredStudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("692c3f01-aed7-4541-b207-d815e5e1efd8"), "carol@aurora.edu", "Carol", "Davis" },
                    { new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), "bob@aurora.edu", "Bob", "Johnson" },
                    { new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), "alice@aurora.edu", "Alice", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721"), "Sophia", "Müller" },
                    { new Guid("15be45ef-25e3-408b-b44e-3393157f70d1"), "Olivia", "Chen" },
                    { new Guid("4fd26caa-574f-4102-b87b-790a5762508f"), "Isabelle", "Kim" },
                    { new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62"), "Elijah", "Patel" },
                    { new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146"), "Liam", "Johnson" },
                    { new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3"), "Ava", "Dubois" },
                    { new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df"), "Emma", "Schmidt" },
                    { new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e"), "Noah", "Silva" },
                    { new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea"), "Mia", "Rossi" },
                    { new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964"), "Lucas", "Garcia" }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "Code", "EndTime", "StartTime", "Title" },
                values: new object[] { new Guid("3c137947-2892-4f57-9eeb-d4931cba03b1"), "TRM1", new DateTime(2025, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Term 1" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Capacity", "Code", "Credits", "StaffId", "TermId", "Title" },
                values: new object[,]
                {
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), 35, "CS103", 3, new Guid("692c3f01-aed7-4541-b207-d815e5e1efd8"), new Guid("3c137947-2892-4f57-9eeb-d4931cba03b1"), "Intro to Programming" },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), 25, "PHYS102", 3, new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), new Guid("3c137947-2892-4f57-9eeb-d4931cba03b1"), "Physics I" },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), 20, "STAT104", 2, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new Guid("3c137947-2892-4f57-9eeb-d4931cba03b1"), "Statistics" },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), 30, "MATH101", 4, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new Guid("3c137947-2892-4f57-9eeb-d4931cba03b1"), "Mathematics I" }
                });

            migrationBuilder.InsertData(
                table: "Assessments",
                columns: new[] { "Id", "Date", "ModuleId", "Title", "Weight" },
                values: new object[,]
                {
                    { new Guid("01e65b87-b32e-43fa-96d6-b2afa0e93154"), new DateTime(2025, 12, 7, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1565), new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), "Data Analysis Report", 0.4m },
                    { new Guid("06a330f0-6c03-46ad-89fc-e3bccb61a4a4"), new DateTime(2025, 11, 12, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1561), new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), "Group Presentation", 0.3m },
                    { new Guid("11a9f45f-1b60-4cf0-a6b0-2944314b3bda"), new DateTime(2025, 12, 2, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1551), new Guid("60f08462-e923-4908-9eea-a27651a14b10"), "Mid-Term Code Review", 0.15m },
                    { new Guid("337ee3fc-b775-4b98-9f67-fcb8b7a20535"), new DateTime(2025, 11, 17, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1548), new Guid("60f08462-e923-4908-9eea-a27651a14b10"), "Programming Project Phase 1", 0.35m },
                    { new Guid("36a2a9b2-ae38-4459-b5ae-867f9542f27c"), new DateTime(2025, 12, 22, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1540), new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), "Physics Final Exam", 0.6m },
                    { new Guid("67b98fcc-e250-49d1-938e-2770a43768df"), new DateTime(2025, 11, 7, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1530), new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), "Lab Report 1", 0.2m },
                    { new Guid("68cb3762-ecf0-416c-903d-593d5f45d9b1"), new DateTime(2026, 1, 1, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1630), new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), "Statistics Final Exam", 0.3m },
                    { new Guid("78fdc1b0-b9ae-4010-827b-c1e77b798f16"), new DateTime(2025, 10, 30, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1506), new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), "Weekly Quizzes", 0.2m },
                    { new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), new DateTime(2025, 11, 17, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1523), new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), "Math Midterm", 0.3m },
                    { new Guid("8c375ce0-db86-4fe7-9ed7-52f555e4b652"), new DateTime(2025, 11, 22, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1534), new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), "Problem Set Submission", 0.2m },
                    { new Guid("995b580b-e65c-4d22-918d-01ea4d1436f8"), new DateTime(2025, 12, 17, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1527), new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), "Math Final Exam", 0.5m },
                    { new Guid("e364808d-834e-4cbe-875f-50f9cccf6268"), new DateTime(2026, 1, 6, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(1555), new Guid("60f08462-e923-4908-9eea-a27651a14b10"), "Final Programming Project", 0.5m }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "EndTime", "Location", "ModuleId", "StaffId", "StartTime" },
                values: new object[,]
                {
                    { new Guid("327cbcfa-8e58-4c7b-9db8-2e621110bae1"), new DateTime(2025, 9, 6, 15, 0, 0, 0, DateTimeKind.Utc), "Lab 205", new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), new DateTime(2025, 9, 6, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new DateTime(2025, 9, 8, 13, 0, 0, 0, DateTimeKind.Utc), "Virtual Classroom", new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("692c3f01-aed7-4541-b207-d815e5e1efd8"), new DateTime(2025, 9, 8, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Lecture Hall C", new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new DateTime(2025, 9, 10, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("41cff357-3345-4b9c-8058-a6aa238da882"), new DateTime(2025, 9, 4, 16, 0, 0, 0, DateTimeKind.Utc), "Seminar Room 101", new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new DateTime(2025, 9, 4, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("59ac3936-4ef7-4826-ba8c-ee60aecc86ef"), new DateTime(2025, 9, 9, 18, 0, 0, 0, DateTimeKind.Utc), "Lab 103", new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("692c3f01-aed7-4541-b207-d815e5e1efd8"), new DateTime(2025, 9, 9, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new DateTime(2025, 9, 5, 11, 0, 0, 0, DateTimeKind.Utc), "Lecture Hall B", new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), new DateTime(2025, 9, 5, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ad029dce-1670-4634-b4bc-fb3c4c421bd0"), new DateTime(2025, 9, 11, 15, 0, 0, 0, DateTimeKind.Utc), "Seminar Room 202", new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new DateTime(2025, 9, 11, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new DateTime(2025, 9, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Lecture Hall A", new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), new DateTime(2025, 9, 3, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "StudentTermModule",
                columns: new[] { "EnrolledModulesId", "EnrolledStudentsId" },
                values: new object[,]
                {
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("60f08462-e923-4908-9eea-a27651a14b10"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("6983138a-93e7-4171-95bc-61ce2fb7cdae"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("7616de2f-756c-4c5e-b49a-8561637c9451"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("b2a8a3ee-ad46-4ef3-80a7-a756e7b4bb07"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") }
                });

            migrationBuilder.InsertData(
                table: "Marks",
                columns: new[] { "Id", "AssessmentId", "Comments", "DateRecorded", "IsResit", "RecordedByStaffId", "Score", "StudentId" },
                values: new object[,]
                {
                    { new Guid("2abd2457-093f-4b51-abae-c633c17ddc0e"), new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2779), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 35m, new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("61077330-02e3-4629-94ce-ae55c3a23471"), new Guid("337ee3fc-b775-4b98-9f67-fcb8b7a20535"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2774), false, null, 95m, new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("6386d3b4-320f-41c4-8312-ededde69a99f"), new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2758), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 92m, new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("6e1a0860-e45e-48a0-930c-04174d9d3678"), new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), "Resit passed.", new DateTime(2025, 12, 2, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2789), true, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 65m, new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("73cc7ed8-a9de-4f71-b427-ce076b311181"), new Guid("337ee3fc-b775-4b98-9f67-fcb8b7a20535"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2864), false, null, 30m, new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("7bbdf417-2e4a-4aa0-b860-1c6c775706a2"), new Guid("8c375ce0-db86-4fe7-9ed7-52f555e4b652"), null, new DateTime(2025, 11, 23, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2839), false, new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), 70m, new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("8ecfe0fb-a26c-4b27-bca9-c30447085ebe"), new Guid("78fdc1b0-b9ae-4010-827b-c1e77b798f16"), null, new DateTime(2025, 10, 28, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2876), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 70m, new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("99b2653f-0e50-494d-83ae-e8eefdf7b77f"), new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2844), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 98m, new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("9d4b93dc-62fd-4934-bae3-4bf5c31adc83"), new Guid("337ee3fc-b775-4b98-9f67-fcb8b7a20535"), "Resit failed to meet passing threshold.", new DateTime(2025, 12, 12, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2870), true, null, 38m, new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("b78e4709-6d92-4d5d-b010-4f4f612f33ef"), new Guid("83fcd674-ce89-4976-9ac1-2ceee1b05adb"), null, new DateTime(2025, 11, 18, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2881), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 75m, new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("cfa7b661-06da-46fc-8814-a6923600e23e"), new Guid("67b98fcc-e250-49d1-938e-2770a43768df"), null, new DateTime(2025, 11, 8, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2767), false, new Guid("99ef4e44-05ae-44ef-937c-24d259268ac4"), 78m, new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("d4cb1ad3-f7d9-425f-b1f0-7f899d57a7ba"), new Guid("06a330f0-6c03-46ad-89fc-e3bccb61a4a4"), null, new DateTime(2025, 11, 13, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2852), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 88m, new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("f40c81e5-1f6a-44ea-a80d-c7f0a80916e4"), new Guid("78fdc1b0-b9ae-4010-827b-c1e77b798f16"), null, new DateTime(2025, 10, 28, 6, 38, 20, 839, DateTimeKind.Utc).AddTicks(2742), false, new Guid("f95c9422-f66f-4c6e-9336-bfd0dccd8880"), 85m, new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") }
                });

            migrationBuilder.InsertData(
                table: "SessionStudent",
                columns: new[] { "RegisteredSessionsId", "RegisteredStudentsId" },
                values: new object[,]
                {
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("3bbc0948-cd32-4768-90d8-6833a3243ae4"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("3f5a49c5-e457-41a6-8270-f52051d1ba92"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("7f74f7b1-5375-4dde-9e78-0efc146e2cb1"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("14ebecc9-c94b-4aa3-a16c-da4e37b8d721") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("15be45ef-25e3-408b-b44e-3393157f70d1") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("4fd26caa-574f-4102-b87b-790a5762508f") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("549a1ad2-8ad5-47f4-bdb1-05ebe8db7c62") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("6cd6224e-06be-4b8b-9000-8c2f896e1146") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("74b46aa9-b25b-4793-bf10-b3adaae5a2b3") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("a88217fb-16ad-4875-9814-b6e739a8a5df") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("c72c0cdf-5c3f-4c07-97ff-7bb3243a4f0e") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("cc3a1dff-3b00-42f0-924a-073b7f2847ea") },
                    { new Guid("be3a92e2-6ace-43cc-ada8-62a1a26874d8"), new Guid("f41bbb0a-72fc-4c2c-850d-9cc621bf9964") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ModuleId",
                table: "Assessments",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_AssessmentId",
                table: "Marks",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_RecordedByStaffId",
                table: "Marks",
                column: "RecordedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentId",
                table: "Marks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_StaffId",
                table: "Modules",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_TermId",
                table: "Modules",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ModuleId",
                table: "Sessions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StaffId",
                table: "Sessions",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionStudent_RegisteredStudentsId",
                table: "SessionStudent",
                column: "RegisteredStudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTermModule_EnrolledStudentsId",
                table: "StudentTermModule",
                column: "EnrolledStudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "SessionStudent");

            migrationBuilder.DropTable(
                name: "StudentTermModule");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
