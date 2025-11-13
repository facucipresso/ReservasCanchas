using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTheRemainingEntitiesToTheDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotField_Field_FieldId",
                table: "TimeSlotField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlotField",
                table: "TimeSlotField");

            migrationBuilder.RenameTable(
                name: "TimeSlotField",
                newName: "timeSlotField");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlotField_FieldId",
                table: "timeSlotField",
                newName: "IX_timeSlotField_FieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timeSlotField",
                table: "timeSlotField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timeSlotField_Field_FieldId",
                table: "timeSlotField",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timeSlotField_Field_FieldId",
                table: "timeSlotField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timeSlotField",
                table: "timeSlotField");

            migrationBuilder.RenameTable(
                name: "timeSlotField",
                newName: "TimeSlotField");

            migrationBuilder.RenameIndex(
                name: "IX_timeSlotField_FieldId",
                table: "TimeSlotField",
                newName: "IX_TimeSlotField_FieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlotField",
                table: "TimeSlotField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotField_Field_FieldId",
                table: "TimeSlotField",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
