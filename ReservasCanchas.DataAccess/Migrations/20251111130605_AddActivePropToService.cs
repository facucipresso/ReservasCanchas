using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservasCanchas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddActivePropToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringCourtBlock");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Service",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RecurringFieldBlock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    WeekDay = table.Column<int>(type: "integer", nullable: false),
                    InitHour = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndHour = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringFieldBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringFieldBlock_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringFieldBlock_FieldId",
                table: "RecurringFieldBlock",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringFieldBlock");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Service");

            migrationBuilder.CreateTable(
                name: "RecurringCourtBlock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    EndHour = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    InitHour = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    WeekDay = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringCourtBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringCourtBlock_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringCourtBlock_FieldId",
                table: "RecurringCourtBlock",
                column: "FieldId");
        }
    }
}
