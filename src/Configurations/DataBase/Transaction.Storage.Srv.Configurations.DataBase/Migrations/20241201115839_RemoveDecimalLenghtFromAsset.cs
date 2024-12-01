using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDecimalLenghtFromAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DecimalSize",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Assets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDateTime",
                table: "AssetTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDateTime",
                table: "AssetTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "AssetTypes",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDateTime",
                table: "Assets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<short>(
                name: "DecimalSize",
                table: "Assets",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDateTime",
                table: "Assets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Assets",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
