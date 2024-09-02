using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Final_Regia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNavigationDependecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Locations_AccountId",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Persons_AccountId",
                table: "Locations",
                column: "AccountId",
                principalTable: "Persons",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Persons_AccountId",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Locations_AccountId",
                table: "Persons",
                column: "AccountId",
                principalTable: "Locations",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
