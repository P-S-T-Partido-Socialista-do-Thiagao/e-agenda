﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class TB_Compromissos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compromissos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDeOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraDeInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraDeTermino = table.Column<TimeSpan>(type: "time", nullable: false),
                    TipoCompromisso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compromissos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compromissos_Contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compromissos_ContatoId",
                table: "Compromissos",
                column: "ContatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compromissos");
        }
    }
}
