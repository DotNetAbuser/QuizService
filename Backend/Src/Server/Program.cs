var builder = WebApplication.CreateBuilder(args);
var configuration  = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false);;

builder.Services.AddDatabase(configuration);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHelpers();

builder.Services.Configure<JwtOptions>(
    configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddJwtAuthentication(configuration);

builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenAnyIP(5001);
}).UseKestrel();

var app = builder.Build();

#if DEBUG
    app.UseSwagger();
    app.UseSwaggerUI();
#endif

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();