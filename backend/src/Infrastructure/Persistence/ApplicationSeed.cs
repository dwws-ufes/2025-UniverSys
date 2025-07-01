namespace UniverSys.Infrastructure.Persistence;
public static class ApplicationSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.Usuarios.Any())
        {
            var admin = new Usuario
            {
                Email = "admin@admin.com.br",
                Login = "admin",    
                Nome = "Admin",
                Tipo = TipoUsuario.Administrador
            };

            var curso = new Curso
            {
                DuracaoSemestres = 8,
                Nome = "Ciência da Computação"
            };

            var departamento = new Departamento
            {
                Nome = "Departamento de Informática"
            };

            var professor = new Professor()
            {
                Nome = "Professor",
                Cpf = "12345678934",
                Departamento = departamento,
                Especializacao = Especializacao.Graduacao
            };

            var aluno = new Aluno 
            {
                Nome = "Aluno",
                Cpf = "12345678967",
                Matricula = "2021100525",
                Curso = curso
            };

            var usuarioProfessor = new Usuario
            {
                Email = "professor@professor.com.br",
                Login = "professor",
                Nome = "Professor",
                Tipo = TipoUsuario.Professor,
                Professor = professor
            };

            var usuarioAluno = new Usuario
            {
                Email = "aluno@aluno.com.br",
                Login = "aluno",
                Nome = "Aluno",
                Tipo = TipoUsuario.Aluno,
                Aluno = aluno
            };

            context.Usuarios.AddRange(admin, usuarioProfessor, usuarioAluno);
            context.Cursos.Add(curso);
            context.Departamentos.Add(departamento);
            context.Professores.Add(professor);
            context.Alunos.Add(aluno);
            
            await context.SaveChangesAsync();
        }
    }
}