using Microsoft.AspNetCore.HttpLogging;
using OAuthTutorial.Code;
using OAuthTutorial.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.secrets.json");

var settings = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(settings);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSingleton<RedirectService>();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("Referer");
    logging.RequestHeaders.Add("Bearer");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure http requests logging:
// https://jorgepsmatos.medium.com/asp-net-logging-http-requests-929c3601c909
// https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging
app.UseHttpLogging();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();