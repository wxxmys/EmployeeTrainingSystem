using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeTrainingSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSchedule",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ClassScheduleName = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Site = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CollegeClass",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ParentIDID = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeClass", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CollegeClass_CollegeClass_ParentIDID",
                        column: x => x.ParentIDID,
                        principalTable: "CollegeClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TrainPlan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Person = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainPlan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TrainQualificationCertificate",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Person = table.Column<string>(nullable: true),
                    CertificateNumber = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    TrainingContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainQualificationCertificate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TrainResource",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainResource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Information",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    ClassScheduleID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Information_ClassSchedule_ClassScheduleID",
                        column: x => x.ClassScheduleID,
                        principalTable: "ClassSchedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainRecord",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Person = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ClassScheduleNameID = table.Column<Guid>(nullable: true),
                    Score = table.Column<string>(nullable: true),
                    TrainContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainRecord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainRecord_ClassSchedule_ClassScheduleNameID",
                        column: x => x.ClassScheduleNameID,
                        principalTable: "ClassSchedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Dateofbirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Accredit = table.Column<byte>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    CollegeClassID = table.Column<Guid>(nullable: true),
                    Degree = table.Column<string>(nullable: true),
                    TeachingDirection = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_CollegeClass_CollegeClassID",
                        column: x => x.CollegeClassID,
                        principalTable: "CollegeClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainRequest",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Person = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    DepartmentNameID = table.Column<Guid>(nullable: true),
                    ClassScheduleNameID = table.Column<Guid>(nullable: true),
                    ApplicationContent = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainRequest_ClassSchedule_ClassScheduleNameID",
                        column: x => x.ClassScheduleNameID,
                        principalTable: "ClassSchedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainRequest_Department_DepartmentNameID",
                        column: x => x.DepartmentNameID,
                        principalTable: "Department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    InformationID = table.Column<Guid>(nullable: true),
                    answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_Information_InformationID",
                        column: x => x.InformationID,
                        principalTable: "Information",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishjob",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CollegeClassID = table.Column<Guid>(nullable: true),
                    TeacherId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    Releasetime = table.Column<DateTime>(nullable: false),
                    InformationID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishjob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Publishjob_CollegeClass_CollegeClassID",
                        column: x => x.CollegeClassID,
                        principalTable: "CollegeClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publishjob_Information_InformationID",
                        column: x => x.InformationID,
                        principalTable: "Information",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publishjob_Member_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingPlans",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CourseSort = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    TeacherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingPlans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeachingPlans_TeachingPlans_ParentID",
                        column: x => x.ParentID,
                        principalTable: "TeachingPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingPlans_Member_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestPaper",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TeacherId = table.Column<Guid>(nullable: true),
                    CollegeClassID = table.Column<Guid>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    TestTime = table.Column<string>(nullable: true),
                    Performance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPaper", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TestPaper_CollegeClass_CollegeClassID",
                        column: x => x.CollegeClassID,
                        principalTable: "CollegeClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestPaper_Member_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CorrectJob",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Markhomeworktime = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    PublishjobID = table.Column<Guid>(nullable: true),
                    Correctingstate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectJob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorrectJob_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectJob_Publishjob_PublishjobID",
                        column: x => x.PublishjobID,
                        principalTable: "Publishjob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectJob_Member_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coursewares",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LinkAddress = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    IMethod = table.Column<int>(nullable: false),
                    Size = table.Column<double>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    TeachingPlanID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coursewares", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Coursewares_TeachingPlans_TeachingPlanID",
                        column: x => x.TeachingPlanID,
                        principalTable: "TeachingPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "examContents",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TestPaperID = table.Column<Guid>(nullable: true),
                    InformationID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examContents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_examContents_Information_InformationID",
                        column: x => x.InformationID,
                        principalTable: "Information",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_examContents_TestPaper_TestPaperID",
                        column: x => x.TestPaperID,
                        principalTable: "TestPaper",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_InformationID",
                table: "Answers",
                column: "InformationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeClass_ParentIDID",
                table: "CollegeClass",
                column: "ParentIDID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectJob_MemberId",
                table: "CorrectJob",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectJob_PublishjobID",
                table: "CorrectJob",
                column: "PublishjobID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectJob_TeacherId",
                table: "CorrectJob",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Coursewares_TeachingPlanID",
                table: "Coursewares",
                column: "TeachingPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_examContents_InformationID",
                table: "examContents",
                column: "InformationID");

            migrationBuilder.CreateIndex(
                name: "IX_examContents_TestPaperID",
                table: "examContents",
                column: "TestPaperID");

            migrationBuilder.CreateIndex(
                name: "IX_Information_ClassScheduleID",
                table: "Information",
                column: "ClassScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CollegeClassID",
                table: "Member",
                column: "CollegeClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishjob_CollegeClassID",
                table: "Publishjob",
                column: "CollegeClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishjob_InformationID",
                table: "Publishjob",
                column: "InformationID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishjob_TeacherId",
                table: "Publishjob",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPlans_ParentID",
                table: "TeachingPlans",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPlans_TeacherId",
                table: "TeachingPlans",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TestPaper_CollegeClassID",
                table: "TestPaper",
                column: "CollegeClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TestPaper_TeacherId",
                table: "TestPaper",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRecord_ClassScheduleNameID",
                table: "TrainRecord",
                column: "ClassScheduleNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRequest_ClassScheduleNameID",
                table: "TrainRequest",
                column: "ClassScheduleNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainRequest_DepartmentNameID",
                table: "TrainRequest",
                column: "DepartmentNameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "CorrectJob");

            migrationBuilder.DropTable(
                name: "Coursewares");

            migrationBuilder.DropTable(
                name: "examContents");

            migrationBuilder.DropTable(
                name: "TrainPlan");

            migrationBuilder.DropTable(
                name: "TrainQualificationCertificate");

            migrationBuilder.DropTable(
                name: "TrainRecord");

            migrationBuilder.DropTable(
                name: "TrainRequest");

            migrationBuilder.DropTable(
                name: "TrainResource");

            migrationBuilder.DropTable(
                name: "Publishjob");

            migrationBuilder.DropTable(
                name: "TeachingPlans");

            migrationBuilder.DropTable(
                name: "TestPaper");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Information");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "ClassSchedule");

            migrationBuilder.DropTable(
                name: "CollegeClass");
        }
    }
}
