using Microsoft.EntityFrameworkCore.Migrations;
using Models;
using System;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SetUpData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Finder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    By = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Playtime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VndbLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OfficialPage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PatchSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DemoVideoUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslationProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Translate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Edit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationProgresses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatchVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfficialLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Download1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Download2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Download3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TutorialVideoLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadDetails_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatchUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatchUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatchUpdates_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStaff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ProjectDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStaff", x => new { x.Id, x.UserId, x.Role });
                    table.ForeignKey(
                        name: "FK_ProjectStaff_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStaff_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadDetails_ProjectDetailId",
                table: "DownloadDetails",
                column: "ProjectDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatchUpdates_ProjectDetailId",
                table: "PatchUpdates",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStaff_ProjectDetailId",
                table: "ProjectStaff",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStaff_UserId",
                table: "ProjectStaff",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationProgresses_ProjectId",
                table: "TranslationProgresses",
                column: "ProjectId",
                unique: true);
            migrationBuilder.InsertData(
    table: "AspNetUsers",
    columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "DisplayName", "JoinDate" },
    values: new object[,]
    {
                    { "1", "admin@example.com", "ADMIN@EXAMPLE.COM", "admin@example.com", "ADMIN@EXAMPLE.COM", true, "", "SAMPLE1", Guid.NewGuid().ToString(), null, false, false, null, true, 0, "Admin User", DateTime.UtcNow },
                    { "2", "pm@example.com", "PM@EXAMPLE.COM", "pm@example.com", "PM@EXAMPLE.COM", true, "", "SAMPLE2", Guid.NewGuid().ToString(), null, false, false, null, true, 0, "Project Manager", DateTime.UtcNow },
                    { "3", "translator@example.com", "TRANSLATOR@EXAMPLE.COM", "translator@example.com", "TRANSLATOR@EXAMPLE.COM", true, "", "SAMPLE3", Guid.NewGuid().ToString(), null, false, false, null, true, 0, "Translator", DateTime.UtcNow },
                    { "4", "editor@example.com", "EDITOR@EXAMPLE.COM", "editor@example.com", "EDITOR@EXAMPLE.COM", true, "", "SAMPLE4", Guid.NewGuid().ToString(), null, false, false, null, true, 0, "Editor", DateTime.UtcNow },
                    { "5", "qa@example.com", "QA@EXAMPLE.COM", "qa@example.com", "QA@EXAMPLE.COM", true, "", "SAMPLE5", Guid.NewGuid().ToString(), null, false, false, null, true, 0, "QA Tester", DateTime.UtcNow }
    });

            // Insert sample projects
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Finder", "Heading", "By", "Thumbnail", "Status", "ShortDescription", "Date", "Link", "Type" },
                values: new object[,]
                {
                    { 1, "Team A", "First Visual Novel", "Developer Studio", "https://example.com/thumb1.jpg", "In Progress", "A short description of the first visual novel project", DateTime.UtcNow.AddDays(-30), "https://example.com/project1", "project" },
                    { 2, "Team B", "Second Game Project", "Another Studio", "https://example.com/thumb2.jpg", "Completed", "A short description of the second game project", DateTime.UtcNow.AddDays(-15), "https://example.com/project2", "project" }
                });

            // Insert translation progress
            migrationBuilder.InsertData(
                table: "TranslationProgresses",
                columns: new[] { "Id", "Translate", "Edit", "QA", "LastUpdated", "ProjectId" },
                values: new object[,]
                {
                    { 1, 75.5m, 50.0m, 25.0m, DateTime.UtcNow, 1 },
                    { 2, 100.0m, 100.0m, 100.0m, DateTime.UtcNow, 2 }
                });

            // Insert project details
            migrationBuilder.InsertData(
                table: "ProjectDetails",
                columns: new[] { "Id", "Publisher", "ReleaseDate", "Playtime", "Genre", "VndbLink", "OfficialPage", "FullDescription", "ProjectId", "PatchSize", "DemoVideoUrl" },
                values: new object[,]
                {
                    { 1, "Publisher X", DateTime.UtcNow.AddYears(-1), "20-30 hours", "Drama, Romance", "https://vndb.org/v1", "https://official.com/game1", "Full detailed description of the first visual novel project", 1, "500MB", "https://youtube.com/demo1" },
                    { 2, "Publisher Y", DateTime.UtcNow.AddYears(-2), "40-50 hours", "Adventure, Mystery", "https://vndb.org/v2", "https://official.com/game2", "Full detailed description of the second game project", 2, "1.2GB", "https://youtube.com/demo2" }
                });

            // Insert download details
            migrationBuilder.InsertData(
                table: "DownloadDetails",
                columns: new[] { "Id", "PatchVersion", "OfficialLink", "Download1", "Download2", "Download3", "TutorialVideoLink", "ProjectDetailId" },
                values: new object[,]
                {
                    { 1, "1.0.0", "https://official.com/patch1", "https://mirror1.com/patch1", "https://mirror2.com/patch1", "https://mirror3.com/patch1", "https://youtube.com/tutorial1", 1 },
                    { 2, "2.0.0", "https://official.com/patch2", "https://mirror1.com/patch2", "https://mirror2.com/patch2", "https://mirror3.com/patch2", "https://youtube.com/tutorial2", 2 }
                });

            // Insert patch updates
            migrationBuilder.InsertData(
                table: "PatchUpdates",
                columns: new[] { "Id", "Version", "Detail", "ReleaseDate", "ProjectDetailId" },
                values: new object[,]
                {
                    { 1, "0.9.0", "Initial beta release with partial translation", DateTime.UtcNow.AddDays(-20), 1 },
                    { 2, "1.0.0", "First stable release with full translation", DateTime.UtcNow.AddDays(-5), 1 },
                    { 3, "1.5.0", "Added bonus content and fixed bugs", DateTime.UtcNow.AddDays(-10), 2 }
                });

            // Insert project staff
            migrationBuilder.InsertData(
                table: "ProjectStaff",
                columns: new[] { "Id", "ProjectDetailId", "UserId", "Role" },
                values: new object[,]
                {
                    { 1, 1, "2", (int)StaffRoleType.ProjectManager },
                    { 2, 1, "3", (int)StaffRoleType.Translator },
                    { 3, 1, "4", (int)StaffRoleType.Editor },
                    { 4, 1, "5", (int)StaffRoleType.QA },
                    { 5, 2, "2", (int)StaffRoleType.ProjectManager },
                    { 6, 2, "3", (int)StaffRoleType.Translator }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DownloadDetails");

            migrationBuilder.DropTable(
                name: "PatchUpdates");

            migrationBuilder.DropTable(
                name: "ProjectStaff");

            migrationBuilder.DropTable(
                name: "TranslationProgresses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
