using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Reservation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Complejo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Complejo_UserId",
                table: "Complejo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_IdUsuario",
                table: "Review",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReservationId",
                table: "Review",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complejo_Usuario_UserId",
                table: "Complejo",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Usuario_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complejo_Usuario_UserId",
                table: "Complejo");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Usuario_UserId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Complejo_UserId",
                table: "Complejo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Complejo");
        }
    }
}
