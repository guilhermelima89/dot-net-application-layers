using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddApiConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiConfiguration(app.Environment);

app.Run();
