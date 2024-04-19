using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorServerEFCoreTest.Migrations
{
    /// <inheritdoc />
    public partial class Project_C_done4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Done = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OGSM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ambition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGSM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Strategies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    First_name = table.Column<string>(type: "text", nullable: false),
                    Last_name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    User_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Actions_In_OGSM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OGSM_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Action_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions_In_OGSM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Actions_In_OGSM_Actions_Action_Id",
                        column: x => x.Action_Id,
                        principalTable: "Actions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actions_In_OGSM_OGSM_OGSM_Id",
                        column: x => x.OGSM_Id,
                        principalTable: "OGSM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals_In_OGSM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OGSM_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Goal_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals_In_OGSM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Goals_In_OGSM_Goals_Goal_Id",
                        column: x => x.Goal_Id,
                        principalTable: "Goals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_In_OGSM_OGSM_OGSM_Id",
                        column: x => x.OGSM_Id,
                        principalTable: "OGSM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strategies_In_OGSM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OGSM_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Strategie_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategies_In_OGSM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Strategies_In_OGSM_OGSM_OGSM_Id",
                        column: x => x.OGSM_Id,
                        principalTable: "OGSM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Strategies_In_OGSM_Strategies_Strategie_Id",
                        column: x => x.Strategie_Id,
                        principalTable: "Strategies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager_and_User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Manager_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager_and_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manager_and_User_Users_Manager_Id",
                        column: x => x.Manager_Id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OGSMs_Per_User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    User_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OGSM_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGSMs_Per_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OGSMs_Per_User_OGSM_OGSM_Id",
                        column: x => x.OGSM_Id,
                        principalTable: "OGSM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OGSMs_Per_User_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_In_OGSM_Action_Id",
                table: "Actions_In_OGSM",
                column: "Action_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_In_OGSM_OGSM_Id",
                table: "Actions_In_OGSM",
                column: "OGSM_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_In_OGSM_Goal_Id",
                table: "Goals_In_OGSM",
                column: "Goal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_In_OGSM_OGSM_Id",
                table: "Goals_In_OGSM",
                column: "OGSM_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_and_User_Manager_Id",
                table: "Manager_and_User",
                column: "Manager_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OGSMs_Per_User_OGSM_Id",
                table: "OGSMs_Per_User",
                column: "OGSM_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OGSMs_Per_User_User_Id",
                table: "OGSMs_Per_User",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Strategies_In_OGSM_OGSM_Id",
                table: "Strategies_In_OGSM",
                column: "OGSM_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Strategies_In_OGSM_Strategie_Id",
                table: "Strategies_In_OGSM",
                column: "Strategie_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions_In_OGSM");

            migrationBuilder.DropTable(
                name: "Goals_In_OGSM");

            migrationBuilder.DropTable(
                name: "Manager_and_User");

            migrationBuilder.DropTable(
                name: "OGSMs_Per_User");

            migrationBuilder.DropTable(
                name: "Strategies_In_OGSM");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OGSM");

            migrationBuilder.DropTable(
                name: "Strategies");
        }
    }
}
