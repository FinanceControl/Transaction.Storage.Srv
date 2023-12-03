using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddNoUniqueIdxHeaderCommitDT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Headers_CommitDateTime",
                table: "Headers",
                column: "CommitDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Headers_CommitDateTime",
                table: "Headers");
        }
    }
}
