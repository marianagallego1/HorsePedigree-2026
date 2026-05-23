using HorsePedigree_2026.Extensions;
using HorsePedigree_2026.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new HorsePedigree_2026.Helpers.OptionalJsonConverterFactory());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Horse Pedigree API",
        Version = "v1",
        Description = "API REST para gestión de pedigrí equino (Supabase / PostgreSQL)."
    });
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationRepositories();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Horse Pedigree API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
