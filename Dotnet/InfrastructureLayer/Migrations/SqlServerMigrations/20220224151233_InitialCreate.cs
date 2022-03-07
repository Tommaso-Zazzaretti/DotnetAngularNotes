using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNet6Mediator.InfrastructureLayer.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Username = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FK_Role = table.Column<int>(type: "integer", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTable_RoleTable_FK_Role",
                        column: x => x.FK_Role,
                        principalTable: "RoleTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoleTable",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "User" });

            migrationBuilder.InsertData(
                table: "RoleTable",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.InsertData(
                table: "UserTable",
                columns: new[] { "Id", "BirthDate", "FK_Role", "Name", "Password", "Surname", "Username" },
                values: new object[] { 1, new DateTime(1996, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Tommaso", "tomPwd", "Zazzaretti", "Tom96" });

            migrationBuilder.InsertData(
                table: "UserTable",
                columns: new[] { "Id", "BirthDate", "FK_Role", "Name", "Password", "Surname", "Username" },
                values: new object[] { 2, new DateTime(1996, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Giorgio", "gioPwd", "Zazzaretti", "Gio96" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_FK_Role",
                table: "UserTable",
                column: "FK_Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "RoleTable");
        }
    }
}
