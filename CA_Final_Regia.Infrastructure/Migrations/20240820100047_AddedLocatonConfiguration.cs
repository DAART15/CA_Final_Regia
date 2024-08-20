using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Final_Regia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocatonConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Persons_AccountId",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HouseNr",
                table: "Locations",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApartmentNr",
                table: "Locations",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AccountId",
                table: "Persons",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AccountId",
                table: "Locations",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Locations_AccountId",
                table: "Persons",
                column: "AccountId",
                principalTable: "Locations",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Locations_AccountId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_AccountId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AccountId",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "HouseNr",
                table: "Locations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentNr",
                table: "Locations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Persons_AccountId",
                table: "Locations",
                column: "AccountId",
                principalTable: "Persons",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
