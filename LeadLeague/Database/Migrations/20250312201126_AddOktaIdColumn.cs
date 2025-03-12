using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadLeague.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOktaIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OktaId",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OktaId",
                table: "Users",
                column: "OktaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_OktaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OktaId",
                table: "Users");
        }
    }
}
