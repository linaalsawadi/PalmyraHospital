using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PalmyraHospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorsAndSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Specializations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_DepartmentId",
                table: "Specializations",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Departments_DepartmentId",
                table: "Specializations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Departments_DepartmentId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_DepartmentId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Specializations");
        }
    }
}
