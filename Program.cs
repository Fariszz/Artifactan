using Artifactan.Jobs;
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config => config.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IArtifactJob, ArtifactJob>();

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

var options = new DashboardOptions
{
    DashboardTitle = "Job Monitor",
    Authorization = new[]
       {
            new HangfireCustomBasicAuthenticationFilter() {
                Pass = "admin",
                User = "admin"
            }
        }
};

app.UseHangfireDashboard("/job", options);
app.MapHangfireDashboard();

// RecurringJob.AddOrUpdate<IArtifactJob>("upload_artifact", (x) => x.UploadArtifact(), "* * * * *");

app.Run();
