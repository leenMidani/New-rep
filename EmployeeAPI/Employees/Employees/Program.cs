using Employees.Data;
using Employees.Model;
using Employees.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<JwTConfig>(builder.Configuration.GetSection("JwTConfig"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddDbContext<EmpDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt => {
}).AddEntityFrameworkStores<EmpDBContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", options =>
    {
        options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration["JwTConfig:Key"]);
        var Issuer = builder.Configuration["JwTConfig:Issuer"];
        var Audience = builder.Configuration["JwTConfig:Audience"];



        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidIssuer=Issuer,
            ValidAudience=Audience

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("default");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
