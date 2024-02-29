using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "Evaluations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Evaluations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudentEntitySubjectEntity",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntitySubjectEntity", x => new { x.StudentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudentEntitySubjectEntity_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEntitySubjectEntity_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentId",
                table: "Evaluations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentEntityId",
                table: "Activities",
                column: "StudentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEntitySubjectEntity_SubjectsId",
                table: "StudentEntitySubjectEntity",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_StudentEntityId",
                table: "Activities",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_StudentEntityId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "StudentEntitySubjectEntity");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StudentEntityId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Activities");
        }
    }
}
