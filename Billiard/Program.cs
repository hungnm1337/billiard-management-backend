
using Billiard.Models;
using Billiard.Repositories.Account;
using Billiard.Services.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Billiard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<Prn232ProjectContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddAuthentication(
            //    option =>
            //    {
            //        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;
            //        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    }

            //    ).AddJwtBearer(option =>
            //    {
            //        option.RequireHttpsMetadata = false;
            //        option.SaveToken = true;
            //        option.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = builder.Configuration["Jwtconfig : Issuer"],
            //            ValidAudience = builder.Configuration["Jwtconfig : Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwtconfig : Key"]!)),
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //        };
            //    });
            //builder.Services.AddAuthorization();

            builder.Services.AddScoped<AccountRepository>();

            builder.Services.AddScoped<AccountService>();

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

            app.Run();
        }
    }
}
