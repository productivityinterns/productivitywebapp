using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductivityApp.Migrations
{
    public partial class modifyAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    zip = table.Column<bool>(nullable: false),
                    EmailAddresses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: true),
                    timeCreated = table.Column<DateTime>(nullable: false),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    prompt = table.Column<string>(nullable: true),
                    tag = table.Column<string>(nullable: true),
                    answer = table.Column<string>(nullable: true),
                    remember = table.Column<bool>(nullable: false),
                    filterId = table.Column<Guid>(nullable: true),
                    SurveyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Field_Filter_filterId",
                        column: x => x.filterId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    inputSurveyId = table.Column<Guid>(nullable: true),
                    destinationId = table.Column<Guid>(nullable: true),
                    IsATemplate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flows_Destination_destinationId",
                        column: x => x.destinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flows_Survey_inputSurveyId",
                        column: x => x.inputSurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    prompt = table.Column<string>(nullable: true),
                    SelectedValue = table.Column<string>(nullable: true),
                    FlowId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criteria_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    fileName = table.Column<string>(nullable: true),
                    kind = table.Column<string>(nullable: true),
                    FlowId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Form_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true),
                    CriteriaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    inputField = table.Column<string>(nullable: true),
                    outputField = table.Column<string>(nullable: true),
                    filterId = table.Column<Guid>(nullable: true),
                    FormId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_Filter_filterId",
                        column: x => x.filterId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_CriteriaId",
                table: "Answer",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_FormId",
                table: "Assignment",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_filterId",
                table: "Assignment",
                column: "filterId");

            migrationBuilder.CreateIndex(
                name: "IX_Criteria_FlowId",
                table: "Criteria",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_SurveyId",
                table: "Field",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_filterId",
                table: "Field",
                column: "filterId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_destinationId",
                table: "Flows",
                column: "destinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_inputSurveyId",
                table: "Flows",
                column: "inputSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_FlowId",
                table: "Form",
                column: "FlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Filter");

            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "Survey");
        }
    }
}
