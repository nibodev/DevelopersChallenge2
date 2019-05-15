using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class FileDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "ImportedFiles",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "FileContent",
                table: "ImportedFiles",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ImportedFiles",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileContent",
                table: "ImportedFiles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
