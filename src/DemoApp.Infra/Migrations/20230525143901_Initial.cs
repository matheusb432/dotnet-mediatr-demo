using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Title = table.Column<string>(
                            type: "varchar(200)",
                            unicode: false,
                            maxLength: 200,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        ListId = table.Column<int>(type: "int", nullable: false),
                        Title = table.Column<string>(
                            type: "varchar(200)",
                            unicode: false,
                            maxLength: 200,
                            nullable: false
                        ),
                        Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Priority = table.Column<int>(type: "int", nullable: false),
                        Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                        Done = table.Column<bool>(type: "bit", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TodoItems");

            migrationBuilder.DropTable(name: "TodoLists");
        }
    }
}
