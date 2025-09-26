using e_commercial.Data;
using e_commercial.Mapping;
using e_commercial.Repositories;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services;
using e_commercial.Services.Interfaces;
using e_commercial.Services.ParentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
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
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IKeyboardRepository, KeyboardRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
//builder.Services.AddScoped<LaptopService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<PaymentService>();
//builder.Services.AddScoped<KeyboardServicce>();

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<KeyboardService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddHostedService<MailService>();

builder.Services.AddSingleton<RabbitMqProducer>();
builder.Services.AddSingleton<RabbitMqConsumer>();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration["ConnectionStrings:RedisHost"]));
// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = builder.Configuration["ConnectionStrings:RedisHost"];
//     // Có thể thêm các tùy chọn khác như instance name, ssl...
//     // options.InstanceName = "MyApp:";
// });
builder.Services.AddScoped<ILaptopService, LaptopService>();


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<LaptopMappingProfile>();
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
});






builder.Services.AddDbContext<ReagvnContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
    System.Console.WriteLine(builder.Environment);
    // Check if we're in test environment
    if (builder.Environment.IsEnvironment("Test"))
    {
        options.UseInMemoryDatabase("TestDatabase");
    }
    else
    {
        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        //modelState: trang thai cua du lieu dau vao sau khi dc binding va validation
        //output: tra ve cac fields bi loi
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value.Errors.Select(e => e.ErrorMessage)
            });

        return new BadRequestObjectResult(new
        {
            StatusCode = 400,
            Message = "Validation failed",
            Details = errors
        });
    };
});


// connection factory
ConnectionFactory _factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "123456"
};

builder.Services.AddSingleton(_factory);
// end 


// channel
using IConnection connection = await _factory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();

builder.Services.AddSingleton(connection);
builder.Services.AddSingleton(channel);
// end

//AsyncEventingBasicConsumer
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
builder.Services.AddSingleton(consumer);

//end

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

//Rate Limit OTP Resend

builder.Services.AddSingleton<PartitionedRateLimiter<HttpContext>>(p =>
{
    return PartitionedRateLimiter.Create<HttpContext, string>(httpContext => //Create<TResource, TPartitionKey>
    {
        var partitionKey = httpContext.User.Identity?.Name
                           ?? httpContext.Connection.RemoteIpAddress?.ToString()
                           ?? "anonymous";

        return RateLimitPartition.GetTokenBucketLimiter(
            partitionKey,
            _ => new TokenBucketRateLimiterOptions
            {
                TokenLimit = 5, //token limit in each bucket
                TokensPerPeriod = 5, //refill token per period
                ReplenishmentPeriod = TimeSpan.FromMinutes(5), //how long to refill
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });
});

//


builder.Services.AddAuthorization();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
//app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }