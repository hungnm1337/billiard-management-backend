using Microsoft.OpenApi.Models;
using Billiard.Models;
using Billiard.Repositories.Account;
using Billiard.Services.Account;
using Billiard.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Billiard.Repositories.Table;
using Billiard.Services.Table;
using Billiard.Repositories.Services;
using Billiard.Services.Service;
using Billiard.Repositories.IBaseRepository;
using Billiard.Services.BaseService;
using Billiard.Services.API;
using Billiard.Services.EmailService;
using Billiard.DTO;
using Billiard.Repositories.Profile;
using Billiard.Services.Profile;
using Billiard.Repositories.Shift;
using Billiard.Services.Shift;

namespace Billiard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===== DATABASE CONFIGURATION =====
            builder.Services.AddDbContext<Prn232ProjectContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ===== CONTROLLERS & API =====
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // ===== SWAGGER CONFIGURATION =====
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Billiard", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập 'Bearer' [space] và sau đó dán token JWT của bạn."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ===== AUTHENTICATION & JWT =====
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            // ===== SESSION CONFIGURATION =====
            builder.Services.AddDistributedMemoryCache();
            // Trong Program.cs - sửa session config
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "BilliardSession";
                options.Cookie.SecurePolicy = CookieSecurePolicy.None; // ✅ Sửa cho development
                options.Cookie.SameSite = SameSiteMode.Lax;
            });


            // ===== CORS CONFIGURATION =====
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // ✅ Specific origin
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
                });
            });

            // ===== DEPENDENCY INJECTION =====
            builder.Services.AddHttpContextAccessor();

            // Repository & Service registrations
            builder.Services.AddScoped<AccountRepository>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServicesService, ServicesService>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService,ProfileService>();
            builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
            builder.Services.AddScoped<IShiftService, ShiftService>();
            // HTTP Clients
            builder.Services.AddHttpClient<WorldNewsService>();
            builder.Services.AddHttpClient<PexelsVideoService>();

            // Email & OTP Services
            builder.Services.AddScoped<IOtpService, OtpService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Other services
            builder.Services.AddScoped<IPasswordHasher<Models.Account>, PasswordHasher<Models.Account>>();

            // Configuration
            builder.Configuration.GetSection("EmailSettings");

            var app = builder.Build();

            // ===== MIDDLEWARE PIPELINE (THỨ TỰ QUAN TRỌNG) =====

            // 1. Development tools
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billiard v1");
                });
            }

            // Always enable Swagger for testing
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billiard v1");
            });

            // 2. HTTPS Redirection (should be early)
            app.UseHttpsRedirection();

            // 3. CORS (before routing)
            app.UseCors();

            // 4. Routing (must be before auth)
            app.UseRouting();

            // 5. Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // 6. Session (AFTER routing, auth)
            app.UseSession();

            // 7. Map controllers (last)
            app.MapControllers();

            app.Run();
        }
    }
}
