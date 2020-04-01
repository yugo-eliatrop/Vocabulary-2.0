using Microsoft.EntityFrameworkCore.Migrations;

namespace Vocabulary.Migrations
{
    public partial class AddLearnStatusToWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLearned",
                table: "Words",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLearned",
                table: "Words");
        }
    }
}
