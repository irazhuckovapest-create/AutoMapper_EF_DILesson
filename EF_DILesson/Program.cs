using EF_DILesson;
using EF_DILesson.Models;
using EF_DILesson.Repository;
using EF_DILesson.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<FilmRepository>();
builder.Services.AddScoped<FilmService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Films.Any())
    {
        context.Films.Add(new Film
        {
            Title = "Бабадук",
            Description = "Мати-одиначка та її син стикаються з моторошною істотою після появи загадкової книжки.",
            ImageUrl = "/posters/babadook.jpg"
        });

        context.Films.Add(new Film
        {
            Title = "Аннабель",
            Description = "Подружжя, що чекає на дитину, опиняється в центрі жахів через стару колекційну ляльку.",
            ImageUrl = "/posters/annabelle.jpg"
        });

        context.Films.Add(new Film
        {
            Title = "Пила",
            Description = "Двоє незнайомців прокидаються в пастці та мають зіграти в жорстоку гру, щоб вижити.",
            ImageUrl = "/posters/saw.jpg"
        });

        context.Films.Add(new Film
        {
            Title = "Обсесія",
            Description = "Хорор про одержимість, яка поступово руйнує життя героїв.",
            ImageUrl = "/posters/obsession.jpg"
        });

        context.Films.Add(new Film
        {
            Title = "3 метри над рівнем неба",
            Description = "Бунтівний хлопець і вихована дівчина з багатої родини закохуються, попри різні світи.",
            ImageUrl = "/posters/three-meters.jpg"
        });

        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();