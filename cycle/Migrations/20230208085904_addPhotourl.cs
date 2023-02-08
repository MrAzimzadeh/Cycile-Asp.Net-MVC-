﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cycle.Migrations
{
    /// <inheritdoc />
    public partial class addPhotourl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Testomonias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Testomonias");
        }
    }
}
