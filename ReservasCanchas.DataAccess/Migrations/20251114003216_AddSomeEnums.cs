using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timeSlotField_Field_FieldId",
                table: "timeSlotField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timeSlotField",
                table: "timeSlotField");

            migrationBuilder.DropIndex(
                name: "IX_timeSlotField_FieldId",
                table: "timeSlotField");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlotComplex_ComplexId",
                table: "TimeSlotComplex");

            migrationBuilder.RenameTable(
                name: "timeSlotField",
                newName: "TimeSlotField");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Complejo",
                newName: "State");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayType",
                table: "Reservation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationState",
                table: "Reservation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "HourPrice",
                table: "Field",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Field",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlotField",
                table: "TimeSlotField",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotField_FieldId",
                table: "TimeSlotField",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotComplex_ComplexId",
                table: "TimeSlotComplex",
                column: "ComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotField_Field_FieldId",
                table: "TimeSlotField",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotField_Field_FieldId",
                table: "TimeSlotField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlotField",
                table: "TimeSlotField");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlotField_FieldId",
                table: "TimeSlotField");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlotComplex_ComplexId",
                table: "TimeSlotComplex");

            migrationBuilder.DropColumn(
                name: "PayType",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ReservationState",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Field");

            migrationBuilder.RenameTable(
                name: "TimeSlotField",
                newName: "timeSlotField");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Complejo",
                newName: "Estado");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Usuario",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "HourPrice",
                table: "Field",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timeSlotField",
                table: "timeSlotField",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_timeSlotField_FieldId",
                table: "timeSlotField",
                column: "FieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotComplex_ComplexId",
                table: "TimeSlotComplex",
                column: "ComplexId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_timeSlotField_Field_FieldId",
                table: "timeSlotField",
                column: "FieldId",
                principalTable: "Field",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
