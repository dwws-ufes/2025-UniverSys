// See https://aka.ms/new-console-template for more information
using System.Reflection;
using UnimedVx.PortalFornecedores.Domain.Entities;

string? entity;
const string? cabecalhoNamespace = "UnimedVx.PortalFornecedores";
string? namespacePadrao;
string? nomeTabela;

Console.WriteLine("Informe a classe que representa a entidade:");
entity = Console.ReadLine();


Assembly libraryAssembly = typeof(Usuario).Assembly;
Type typeUsuario = typeof(Usuario);
Type? type = libraryAssembly.GetType($"{typeUsuario.Namespace}.{entity}");

if (type == null)
{
    Console.WriteLine("Entidade não encontrada");
    return;
}

Console.WriteLine($"Informe como deseja a pasta e namespace. ({entity}s)");
namespacePadrao = Console.ReadLine();

Console.Write($"Informe o nome da tabela na base de dados: ({entity}s)");
nomeTabela = Console.ReadLine();

if (string.IsNullOrEmpty(namespacePadrao))
    namespacePadrao = $"{entity}s";

if (string.IsNullOrEmpty(nomeTabela))
    nomeTabela = $"{entity}s";

UpdateApplicationContext(entity, nomeTabela);
CreateVm(cabecalhoNamespace, entity, namespacePadrao, type);
CreateSalvarCommand(cabecalhoNamespace, entity, nomeTabela, namespacePadrao, type);
CreateEntityConfiguration(cabecalhoNamespace, entity);
CreateObterQuery(cabecalhoNamespace, entity, nomeTabela, namespacePadrao);
CreateObterPorIdQuery(cabecalhoNamespace, entity, nomeTabela, namespacePadrao);
CreateExcluirCommand(cabecalhoNamespace, entity, nomeTabela, namespacePadrao);
CreateController(cabecalhoNamespace, entity, nomeTabela, namespacePadrao);

Console.Write($"ARTEFATOS GERADOS COM SUCESSO");


static void CreateVm(string cabecalhoNamespace, string? entity, string? namespacePadrao, Type type)
{
    string propriedades = ObterPropriedades(cabecalhoNamespace, type);

    var template = File.ReadAllText("TemplateVm.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#propriedades#", propriedades);

    var caminhoApplication = $"../../../../Application/Requests/{namespacePadrao}";
    var caminhoApplicationCommands = $"{caminhoApplication}/Models";
    if (!Directory.Exists(caminhoApplicationCommands))
        Directory.CreateDirectory(caminhoApplicationCommands);

    var caminhoComando = $"{caminhoApplicationCommands}/{entity}Vm.cs";
    //File.Create(caminhoVm, 2048, FileOptions.);
    File.WriteAllText(caminhoComando, template);
}

static void CreateSalvarCommand(string cabecalhoNamespace, string? entity, string? nomeTabela, string? namespacePadrao, Type type)
{
    var propriedades = ObterPropriedades(cabecalhoNamespace, type);

    var template = File.ReadAllText("TemplateSalvarCommand.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#nomeTabela#", nomeTabela);
    template = template.Replace("#propriedades#", propriedades);

    var caminhoApplication = $"../../../../Application/Requests/{namespacePadrao}";
    var caminhoApplicationCommands = $"{caminhoApplication}/Commands";
    if (!Directory.Exists(caminhoApplicationCommands))
        Directory.CreateDirectory(caminhoApplicationCommands);

    var caminhoComando = $"{caminhoApplicationCommands}/{entity}SalvarCommand.cs";
    //File.Create(caminhoVm, 2048, FileOptions.);
    File.WriteAllText(caminhoComando, template);
}

static void UpdateApplicationContext(string? entity, string? nomeTabela)
{
    var caminhoInterfaceContext = $"../../../../Application/Common/Interfaces/IApplicationDbContext.cs";
    var caminhoContext = $"../../../../Infrastructure/Persistence/ApplicationDbContext.cs";

    var textoIncluir = @$"DbSet<{entity}> {nomeTabela} {{ get; set; }}
//LOCAL PROPRIEDADES GERADAS PELO SCAFFOLD (NÃO EXCLUIR)";

    var textoIncluirContext = @$"public DbSet<{entity}> {nomeTabela} {{ get; set; }}
//LOCAL PROPRIEDADES GERADAS PELO SCAFFOLD (NÃO EXCLUIR)";

    var templateInterface = File.ReadAllText(caminhoInterfaceContext);
    templateInterface = templateInterface.Replace("//LOCAL PROPRIEDADES GERADAS PELO SCAFFOLD (NÃO EXCLUIR)", textoIncluir);

    var templateContexto = File.ReadAllText(caminhoContext);
    templateContexto = templateContexto.Replace("//LOCAL PROPRIEDADES GERADAS PELO SCAFFOLD (NÃO EXCLUIR)", textoIncluirContext);

    File.WriteAllText(caminhoInterfaceContext, templateInterface);
    File.WriteAllText(caminhoContext, templateContexto);
}

static string ObterPropriedades(string cabecalhoNamespace, Type type)
{
    var propriedades = "";
    foreach (var prop in type.GetProperties())
    {
        if (prop.PropertyType.FullName != null
            && (prop.PropertyType.FullName.StartsWith("System")
                || prop.PropertyType.FullName.StartsWith(cabecalhoNamespace)))
        {
            string nomeTipo = "";

            if (prop.PropertyType.IsGenericType &&
                prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var tipo = prop.PropertyType.GetGenericArguments()[0];
                nomeTipo = $"{tipo.Name}?";
            }
            else
                nomeTipo = prop.PropertyType.Name;


            if (prop.Name == "Id")
                nomeTipo += "?";

            propriedades += $"\tpublic {nomeTipo} {prop.Name} {{ get; set; }}\r\n";

        }
    }

    return propriedades;
}

static void CreateEntityConfiguration(string cabecalhoNamespace, string? entity)
{
    var template = File.ReadAllText("TemplateEntityConfiguration.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#entidade#", entity);

    var caminhoArquivo = $"../../../../Infrastructure/Persistence/Configurations";

    if (!Directory.Exists(caminhoArquivo))
        Directory.CreateDirectory(caminhoArquivo);

    var caminhoComando = $"{caminhoArquivo}/{entity}Configuration.cs";
    File.WriteAllText(caminhoComando, template);
}

static void CreateObterQuery(string cabecalhoNamespace, string? entity, string? nomeTabela, string? namespacePadrao)
{

    var template = File.ReadAllText("TemplateObterQuery.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#nomeTabela#", nomeTabela);

    var caminhoApplication = $"../../../../Application/Requests/{namespacePadrao}/Queries";

    if (!Directory.Exists(caminhoApplication))
        Directory.CreateDirectory(caminhoApplication);

    var caminhoQuery = $"{caminhoApplication}/{entity}ObterQuery.cs";
    //File.Create(caminhoVm, 2048, FileOptions.);
    File.WriteAllText(caminhoQuery, template);
}

static void CreateObterPorIdQuery(string cabecalhoNamespace, string? entity, string? nomeTabela, string? namespacePadrao)
{

    var template = File.ReadAllText("TemplateObterPorIdQuery.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#nomeTabela#", nomeTabela);

    var caminhoApplication = $"../../../../Application/Requests/{namespacePadrao}/Queries";

    if (!Directory.Exists(caminhoApplication))
        Directory.CreateDirectory(caminhoApplication);

    var caminhoQuery = $"{caminhoApplication}/{entity}ObterPorIdQuery.cs";
    //File.Create(caminhoVm, 2048, FileOptions.);
    File.WriteAllText(caminhoQuery, template);
}

static void CreateExcluirCommand(string cabecalhoNamespace, string? entity, string? nomeTabela, string? namespacePadrao)
{

    var template = File.ReadAllText("TemplateExcluirCommand.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#nomeTabela#", nomeTabela);

    var caminhoApplication = $"../../../../Application/Requests/{namespacePadrao}/Commands";

    if (!Directory.Exists(caminhoApplication))
        Directory.CreateDirectory(caminhoApplication);

    var caminhoQuery = $"{caminhoApplication}/{entity}ExcluirCommand.cs";
    //File.Create(caminhoVm, 2048, FileOptions.);
    File.WriteAllText(caminhoQuery, template);
}

static void CreateController(string cabecalhoNamespace, string? entity, string? nomeTabela, string? namespacePadrao)
{

    var template = File.ReadAllText("TemplateController.txt");
    template = template.Replace("#cabecalhoNamespace#", cabecalhoNamespace);
    template = template.Replace("#namespace#", namespacePadrao);
    template = template.Replace("#entidade#", entity);
    template = template.Replace("#nomeTabela#", nomeTabela);

    var caminho = $"../../../../WebUI/Controllers/{nomeTabela}Controller.cs";
    File.WriteAllText(caminho, template);
}
