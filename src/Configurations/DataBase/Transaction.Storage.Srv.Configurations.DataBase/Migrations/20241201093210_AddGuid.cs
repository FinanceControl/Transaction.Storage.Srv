using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "CounterParties",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "CloseDate",
                table: "Accounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "KeepassId",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastSyncDate",
                table: "Accounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "CounterPartyTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "CounterParties");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "KeepassId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastSyncDate",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "CounterPartyTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "LegalEntity");
        }
    }
}
