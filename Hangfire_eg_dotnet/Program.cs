using Hangfire;
using Hangfire_eg_dotnet.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHangfire(x =>
{
    x.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServerDBConnection"));
});

builder.Services.AddHangfireServer();
builder.Services.AddScoped<IJobService,JobService>();  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
