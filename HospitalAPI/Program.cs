using HospitalApi.Services;
using HospitalApi.Mapping;
using HospitalApi.Models;
using HospitalApi.SwaggerExamples;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role 
    };
});


builder.Services.AddAuthorization();


// DbContext MariaDb
builder.Services.AddDbContext<ApplicationDbContext>(options =>  
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(11, 4, 2))));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<HabitacionService>();
builder.Services.AddScoped<CamaService>();
builder.Services.AddScoped<HistorialAltaService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<IngresoService>();
builder.Services.AddScoped<AsignacionService>();


// Add Swagger
builder.Services.AddEndpointsApiExplorer();

// Swagger con soporte para JWT

builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalApi", Version = "v1" });

    
    // Añadir configuración de seguridad JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en este formato: Bearer {token}"
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
    

    // Incluir comentarios XML para la documentación de Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Registrar los ejemplos
builder.Services.AddSwaggerExamplesFromAssemblyOf<RolDTOListExample>();

// Json Settings
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS before authorization and other middlewares
app.UseCors("AllowSpecificOrigin");


app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
