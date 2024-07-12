using Server.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ConnectDB>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.Run();
