using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class ConvertIntAmountToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Positions",
                type: "numeric(30,15)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Positions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(30,15)");
        }
    }
}
