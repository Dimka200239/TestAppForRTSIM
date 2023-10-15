using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class NewInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    abbreviation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    meaning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.abbreviation);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    orgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameOrg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.orgId);
                });

            migrationBuilder.CreateTable(
                name: "Userss",
                columns: table => new
                {
                    loginUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passwordUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userss", x => x.loginUser);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    loginEmp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fisrstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordEmp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    abbreviation = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.loginEmp);
                    table.ForeignKey(
                        name: "FK_Employees_Genders_abbreviation",
                        column: x => x.abbreviation,
                        principalTable: "Genders",
                        principalColumn: "abbreviation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentss",
                columns: table => new
                {
                    commentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    loginUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentss", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_Commentss_Userss_loginUser",
                        column: x => x.loginUser,
                        principalTable: "Userss",
                        principalColumn: "loginUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOrganizationMaps",
                columns: table => new
                {
                    loginEmp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    orgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOrganizationMaps", x => new { x.loginEmp, x.orgId });
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizationMaps_Employees_loginEmp",
                        column: x => x.loginEmp,
                        principalTable: "Employees",
                        principalColumn: "loginEmp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizationMaps_Organizations_orgId",
                        column: x => x.orgId,
                        principalTable: "Organizations",
                        principalColumn: "orgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentss_loginUser",
                table: "Commentss",
                column: "loginUser");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrganizationMaps_orgId",
                table: "EmployeeOrganizationMaps",
                column: "orgId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_abbreviation",
                table: "Employees",
                column: "abbreviation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentss");

            migrationBuilder.DropTable(
                name: "EmployeeOrganizationMaps");

            migrationBuilder.DropTable(
                name: "Userss");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
