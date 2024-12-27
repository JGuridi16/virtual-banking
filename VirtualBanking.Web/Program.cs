using VirtualBanking.Web.Hubs;
using VirtualBanking.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(nameof(CorsSettings)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var corsSettings = builder.Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(corsSettings?.AllowedOrigins ?? [])
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.UseWebSockets();

app.MapHub<SmartQueueHub>("/smartqueue").RequireCors(policy =>
{
    policy.WithOrigins(corsSettings?.AllowedOrigins ?? [])
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
});

app.MapControllers();

app.Run();
