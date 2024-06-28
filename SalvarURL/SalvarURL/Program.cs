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
    var urlData = await context.Request.ReadFromJsonAsync<UrlData>();

    if (urlData == null || string.IsNullOrEmpty(urlData.UrlPublica) || string.IsNullOrEmpty(urlData.EmailUsuario))
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Dados inválidos");
        return;
    }

    await dbContext.SaveUrlAsync(urlData);
    context.Response.StatusCode = StatusCodes.Status201Created;
});

app.Run();

public class UrlData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UrlPublica { get; set; }
    public string EmailUsuario { get; set; }
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}

public interface IMongoDbContext
{
    Task SaveUrlAsync(UrlData urlData);
}

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoCollection<UrlData> _urlsCollection;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _urlsCollection = mongoDatabase.GetCollection<UrlData>(mongoDbSettings.Value.CollectionName);
    }

    public async Task SaveUrlAsync(UrlData urlData)
    {
        await _urlsCollection.InsertOneAsync(urlData);
    }
}
