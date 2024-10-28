using ApplicationCore.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add a custom scoped service.
builder.Services.AddDbContext<SeamlessDigitalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SeamlessDigitalContext")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddHttpContextAccessor();

//services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
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
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Category}/{action=AddCategory}/{id?}");
});

app.Run();
