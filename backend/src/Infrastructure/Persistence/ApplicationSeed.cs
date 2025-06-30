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

            var professor = new Usuario
            {
                Email = "professor@professor.com.br",
                Login = "professor",
                Nome = "Professor",
                Tipo = TipoUsuario.Professor
            };

            var aluno = new Usuario
            {
                Email = "aluno@aluno.com.br",
                Login = "aluno",
                Nome = "Aluno",
                Tipo = TipoUsuario.Aluno
            };

            context.Usuarios.AddRange(admin, professor, aluno);
            await context.SaveChangesAsync();
        }
    }
}