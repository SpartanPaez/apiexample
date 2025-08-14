using WebApi.Domain.Repositories.Read;
using WebApi.Domain.Repositories.Write;
using WebApi.Infrastructure.Persistence;
using WebApi.Infrastructure.Persistence.Read;
using WebApi.Infrastructure.Persistence.Write;


var builder = WebApplication.CreateBuilder(args);
// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<SpartanDbContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("SpartanDbConnection")!));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddScoped<IShoeReadRepository, ShoeReadRepository>();
builder.Services.AddScoped<IShoeWriteRepository, ShoeWriteRepository>();
builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
builder.Services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    c.SerializeAsV2 = false;
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        var filePath = Path.Combine(app.Environment.WebRootPath, "swagger.json");
        File.WriteAllText(filePath, System.Text.Json.JsonSerializer.Serialize(swaggerDoc));
    });
});

app.UseSwaggerUI();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
