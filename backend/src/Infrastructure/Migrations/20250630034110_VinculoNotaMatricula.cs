using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniverSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VinculoNotaMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Alunos_AlunoId",
                table: "Notas");

            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "Notas",
                newName: "MatriculaId");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_AlunoId",
                table: "Notas",
                newName: "IX_Notas_MatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Matriculas_MatriculaId",
                table: "Notas",
                column: "MatriculaId",
                principalTable: "Matriculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Matriculas_MatriculaId",
                table: "Notas");

            migrationBuilder.RenameColumn(
                name: "MatriculaId",
                table: "Notas",
                newName: "AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_MatriculaId",
                table: "Notas",
                newName: "IX_Notas_AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Alunos_AlunoId",
                table: "Notas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
