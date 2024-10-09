using FluentValidation;
using Users.Models.Entities;
using Users.Services.UserService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var corsEnabled = builder.Configuration.GetValue<bool>("CorsPolicy:Enabled");
if (corsEnabled)
{
    var originsConfig = builder.Configuration.GetSection("CorsPolicy:Origins").Get<string>();
    var originList = originsConfig?.Split('|');

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            policy =>
            {
                policy.WithOrigins(originList)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
    });
}

// You can also configure named clients with specific settings
builder.Services.AddHttpClient("MailerApi", c => { c.BaseAddress = new Uri(builder.Configuration.GetSection("MailerAPIEndpoint").Get<string>()); });


// Add services to the container.

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

if (corsEnabled)
{
    app.UseCors("CorsPolicy");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
