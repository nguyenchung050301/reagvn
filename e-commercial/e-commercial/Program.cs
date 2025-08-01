using e_commercial.Data;
using e_commercial.Models.Products;
using e_commercial.Repositories;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services;
using e_commercial.Services.InterfaceService;
using e_commercial.Services.ServiceFactory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
builder.Services.AddScoped<IKeyboardRepository, KeyboardRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
//builder.Services.AddScoped<LaptopService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<RefreshTokenService>();
//builder.Services.AddScoped<KeyboardServicce>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<IGenericCartProductService>( p => 
{ 
    var db = p.GetRequiredService<ReagvnContext>();
    var cartService = p.GetRequiredService<CartService>();
    return new GenericCartProductService<Laptop>(db, cartService,
        laptop => (laptop.LaptopId.ToString(), laptop.Category.CategoryId ,(float)laptop.Price, (int)laptop.StockQuantity), "Laptop");
});
builder.Services.AddScoped<CartProductServiceFactory>();
builder.Services.AddDbContext<ReagvnContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnection"))
    )
);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<AuthenticationSchemeOptions, 
 //   MyAppAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (opt) => { });
var publicKey = builder.Configuration["JWT:PublicKeyPath"];
using var rsa = RSA.Create();
rsa.ImportFromPem(File.ReadAllText(publicKey).ToCharArray());
var rsaKey = new RsaSecurityKey(rsa);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = rsaKey,    
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // Disable clock skew to ensure token expiration is precise
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
