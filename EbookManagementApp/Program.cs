using AppSettings;
using EbookManagementApp.Extensions;
using Services.Interface;
using Services.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IBooks, BookService>();
builder.Services.AddScoped<IAuthor, AuthorService>();
builder.Services.AddScoped<ISearch, SearchService>();
builder.Services.AddScoped<ILogin, RegisterLoginService>();
builder.Services.Configure<JwtClaimDetails>(builder.Configuration.GetSection("Jwt"));
//builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerAuthorization();
builder.Services.AuthenticationExtensions(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
