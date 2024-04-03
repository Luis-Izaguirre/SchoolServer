using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_description = table.Column<string>(type: "text", nullable: false),
                    instructor_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    student_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    major = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    student_year = table.Column<int>(type: "int", nullable: true),
                    course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.student_id);
                    table.ForeignKey(
                        name: "FK_Students_Courses",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_course_id",
                table: "Students",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
