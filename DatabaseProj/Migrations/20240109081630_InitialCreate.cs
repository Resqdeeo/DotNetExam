using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseProj.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    HealthPoints = table.Column<int>(type: "integer", nullable: false),
                    AttackModifier = table.Column<int>(type: "integer", nullable: false),
                    AttackPerRound = table.Column<int>(type: "integer", nullable: false),
                    Damage = table.Column<string>(type: "text", nullable: true),
                    DamageModifier = table.Column<int>(type: "integer", nullable: false),
                    Ac = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Ac", "AttackModifier", "AttackPerRound", "Damage", "DamageModifier", "HealthPoints", "Name" },
                values: new object[,]
                {
                    { 1, 10, 0, 1, "1d6", 2, 10, "Скелет Бладфина" },
                    { 2, 8, 2, 1, "2d6", 3, 25, "Aarakocra" },
                    { 3, 20, 5, 2, "3d3", 2, 15, "Wizard" },
                    { 4, 6, 1, 2, "2d5", 2, 30, "Acererak" },
                    { 5, 10, 2, 1, "2d6", 3, 25, "Dragon" },
                    { 6, 4, 5, 3, "2d3", 3, 15, "Assasin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monsters");
        }
    }
}
