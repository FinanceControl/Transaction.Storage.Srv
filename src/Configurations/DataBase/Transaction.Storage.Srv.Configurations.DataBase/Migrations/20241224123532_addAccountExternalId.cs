using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class addAccountExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_CounterPartyId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CounterPartyId_ExternalId",
                table: "Accounts",
                columns: new[] { "CounterPartyId", "ExternalId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_CounterPartyId_ExternalId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CounterPartyId",
                table: "Accounts",
                column: "CounterPartyId");
        }
    }
}
