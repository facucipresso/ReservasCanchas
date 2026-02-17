using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCancelationReasonToComplex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelationReason",
                table: "Complex",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelationReason",
                table: "Complex");
        }
    }
}
