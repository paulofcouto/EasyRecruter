using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

//Configurar MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

var app = builder.Build();

app.MapPost("/SalvarUrl", async (HttpContext context, IMongoDbContext dbContext) =>
{
    var pessoaViewModel = await context.Request.ReadFromJsonAsync<PessoaViewModel>();

    pessoaViewModel.Usuario = "Paulo"; //obter usuario do token JWT
    if (pessoaViewModel == null || string.IsNullOrEmpty(pessoaViewModel.Url) || string.IsNullOrEmpty(pessoaViewModel.Usuario))
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Dados inválidos");
        return;
    }

    await dbContext.SaveUrlAsync(pessoaViewModel);
    context.Response.StatusCode = StatusCodes.Status201Created;
});

app.Run();

public class PessoaViewModel
{
    public string Url { get; set; }
    public string Usuario { get; set; }
    public string Nome { get; set; }
    public string Cargo { get; set; }
    public string Sobre { get; set; }
    public List<ExperienciaViewModel> Experiencias { get; set; }
    public List<FormacaoAcademicaViewModel> FormacaoAcademica { get; set; }
}

public class ExperienciaViewModel
{
    public string Cargo { get; set; }
    public string Empresa { get; set; }
    public string Periodo { get; set; }
    public string Local { get; set; }
    public string Descricao { get; set; }
}

public class FormacaoAcademicaViewModel
{
    public string Instituicao { get; set; }
    public string Curso { get; set; }
    public string Periodo { get; set; }
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}

public interface IMongoDbContext
{
    Task SaveUrlAsync(PessoaViewModel pessoaViewModel);
}

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoCollection<PessoaViewModel> _urlsCollection;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _urlsCollection = mongoDatabase.GetCollection<PessoaViewModel>(mongoDbSettings.Value.CollectionName);
    }

    public async Task SaveUrlAsync(PessoaViewModel pessoaViewModel)
    {
        await _urlsCollection.InsertOneAsync(pessoaViewModel);
    }
}
