using Microsoft.EntityFrameworkCore;
using SuperheroApp.Core.Interfaces;
using SuperheroApp.Infrastructure.Data;
using SuperheroApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para escutar nas portas 80 e 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
    options.ListenAnyIP(5000);
});

// Adicionar serviços ao conteiner
builder.Services.AddControllers();

// Adicionar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar repositórios
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<ISuperpowerRepository, SuperpowerRepository>();

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Superhero API",
        Version = "v1",
        Description = "A Web API for managing superheroes and their superpowers",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "API Support",
            Email = "support@superheroapp.com"
        }
    });
    
    // Incluir comentários XML do projeto API
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
    
    // Incluir comentários XML do projeto Core
    var coreXmlFile = "SuperheroApp.Core.xml";
    var coreXmlPath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);
    if (File.Exists(coreXmlPath))
    {
        options.IncludeXmlComments(coreXmlPath);
    }
});

var app = builder.Build();

// Configura a pipeline de HTTP request.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Superhero API v1");
        options.RoutePrefix = string.Empty; 
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); 
        options.DefaultModelsExpandDepth(0); 
        options.EnableFilter();
        options.EnableDeepLinking();
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Superhero API v1");
        options.RoutePrefix = "api-docs"; 
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        options.DefaultModelsExpandDepth(0);
        options.EnableFilter();
        options.EnableDeepLinking();
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

app.Run();