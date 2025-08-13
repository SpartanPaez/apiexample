using WebApi.Domain.Repositories.Read;
using WebApi.Domain.Repositories.Write;
using WebApi.Infrastructure.Persistence;
using WebApi.Infrastructure.Persistence.Read;
using WebApi.Infrastructure.Persistence.Write;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SpartanDbContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("SpartanDbConnection")!));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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



// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// FluentValidation
// builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Entity Framework - Comentado hasta crear los DbContext
// builder.Services.AddDbContext<WriteDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddDbContext<ReadDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CQRS Pipeline Behaviors - Comentado hasta crear las clases
// builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
// builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Repositories - Comentado hasta crear las interfaces e implementaciones
// Write repositories (Commands)
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Read repositories (Queries)
// builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
