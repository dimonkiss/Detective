using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detective.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.AgencyId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "AgencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detectives",
                columns: table => new
                {
                    DetectiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detectives", x => x.DetectiveId);
                    table.ForeignKey(
                        name: "FK_Detectives_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "AgencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetectiveId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DetectiveId1 = table.Column<int>(type: "int", nullable: true),
                    ClientId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Cases_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Cases_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Cases_Detectives_DetectiveId",
                        column: x => x.DetectiveId,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId");
                    table.ForeignKey(
                        name: "FK_Cases_Detectives_DetectiveId1",
                        column: x => x.DetectiveId1,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetectiveId1 = table.Column<int>(type: "int", nullable: true),
                    CaseId1 = table.Column<int>(type: "int", nullable: true),
                    DetectiveId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Cases_CaseId1",
                        column: x => x.CaseId1,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                    table.ForeignKey(
                        name: "FK_Orders_Detectives_DetectiveId",
                        column: x => x.DetectiveId,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Detectives_DetectiveId1",
                        column: x => x.DetectiveId1,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetectiveId1 = table.Column<int>(type: "int", nullable: true),
                    CaseId1 = table.Column<int>(type: "int", nullable: true),
                    DetectiveId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Cases_CaseId1",
                        column: x => x.CaseId1,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                    table.ForeignKey(
                        name: "FK_Reports_Detectives_DetectiveId",
                        column: x => x.DetectiveId,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Detectives_DetectiveId1",
                        column: x => x.DetectiveId1,
                        principalTable: "Detectives",
                        principalColumn: "DetectiveId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId",
                table: "Cases",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId1",
                table: "Cases",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DetectiveId",
                table: "Cases",
                column: "DetectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DetectiveId1",
                table: "Cases",
                column: "DetectiveId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AgencyId",
                table: "Clients",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Detectives_AgencyId",
                table: "Detectives",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CaseId",
                table: "Orders",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CaseId1",
                table: "Orders",
                column: "CaseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DetectiveId",
                table: "Orders",
                column: "DetectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DetectiveId1",
                table: "Orders",
                column: "DetectiveId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CaseId",
                table: "Reports",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CaseId1",
                table: "Reports",
                column: "CaseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_DetectiveId",
                table: "Reports",
                column: "DetectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_DetectiveId1",
                table: "Reports",
                column: "DetectiveId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Detectives");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
