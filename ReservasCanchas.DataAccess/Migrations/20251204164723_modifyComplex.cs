using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyComplex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complejo_Users_UserId",
                table: "Complejo");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplexService_Complejo_ComplexesId",
                table: "ComplexService");

            migrationBuilder.DropForeignKey(
                name: "FK_Field_Complejo_ComplexId",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotComplex_Complejo_ComplexId",
                table: "TimeSlotComplex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complejo",
                table: "Complejo");

            migrationBuilder.RenameTable(
                name: "Complejo",
                newName: "Complex");

            migrationBuilder.RenameIndex(
                name: "IX_Complejo_UserId",
                table: "Complex",
                newName: "IX_Complex_UserId");

            migrationBuilder.AddColumn<string>(
                name: "CBU",
                table: "Complex",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complex",
                table: "Complex",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complex_Users_UserId",
                table: "Complex",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplexService_Complex_ComplexesId",
                table: "ComplexService",
                column: "ComplexesId",
                principalTable: "Complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Complex_ComplexId",
                table: "Field",
                column: "ComplexId",
                principalTable: "Complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotComplex_Complex_ComplexId",
                table: "TimeSlotComplex",
                column: "ComplexId",
                principalTable: "Complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complex_Users_UserId",
                table: "Complex");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplexService_Complex_ComplexesId",
                table: "ComplexService");

            migrationBuilder.DropForeignKey(
                name: "FK_Field_Complex_ComplexId",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotComplex_Complex_ComplexId",
                table: "TimeSlotComplex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complex",
                table: "Complex");

            migrationBuilder.DropColumn(
                name: "CBU",
                table: "Complex");

            migrationBuilder.RenameTable(
                name: "Complex",
                newName: "Complejo");

            migrationBuilder.RenameIndex(
                name: "IX_Complex_UserId",
                table: "Complejo",
                newName: "IX_Complejo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complejo",
                table: "Complejo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complejo_Users_UserId",
                table: "Complejo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplexService_Complejo_ComplexesId",
                table: "ComplexService",
                column: "ComplexesId",
                principalTable: "Complejo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Complejo_ComplexId",
                table: "Field",
                column: "ComplexId",
                principalTable: "Complejo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotComplex_Complejo_ComplexId",
                table: "TimeSlotComplex",
                column: "ComplexId",
                principalTable: "Complejo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
