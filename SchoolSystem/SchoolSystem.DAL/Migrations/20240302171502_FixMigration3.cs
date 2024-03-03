using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_StudentEntityId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StudentEntityId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentEntityId",
                table: "Activities",
                column: "StudentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_StudentEntityId",
                table: "Activities",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
