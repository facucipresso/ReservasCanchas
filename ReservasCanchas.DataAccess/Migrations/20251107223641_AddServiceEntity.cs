using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplexService_Complejo_ComplexId",
                table: "ComplexService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplexService",
                table: "ComplexService");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "ComplexService");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ComplexService",
                newName: "ServicesId");

            migrationBuilder.RenameColumn(
                name: "ComplexId",
                table: "ComplexService",
                newName: "ComplexesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplexService",
                table: "ComplexService",
                columns: new[] { "ComplexesId", "ServicesId" });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplexService_ServicesId",
                table: "ComplexService",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplexService_Complejo_ComplexesId",
                table: "ComplexService",
                column: "ComplexesId",
                principalTable: "Complejo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplexService_Service_ServicesId",
                table: "ComplexService",
                column: "ServicesId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplexService_Complejo_ComplexesId",
                table: "ComplexService");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplexService_Service_ServicesId",
                table: "ComplexService");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplexService",
                table: "ComplexService");

            migrationBuilder.DropIndex(
                name: "IX_ComplexService_ServicesId",
                table: "ComplexService");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "ComplexService",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ComplexesId",
                table: "ComplexService",
                newName: "ComplexId");

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "ComplexService",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplexService",
                table: "ComplexService",
                columns: new[] { "ComplexId", "Service" });

            migrationBuilder.AddForeignKey(
                name: "FK_ComplexService_Complejo_ComplexId",
                table: "ComplexService",
                column: "ComplexId",
                principalTable: "Complejo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
