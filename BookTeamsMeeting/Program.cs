using BookTeamsMeeting.Services;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var scopes = new List<string>() { "User.read", "Mail.Send", "Mail.ReadWrite", "OnlineMeetings.ReadWrite" };

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph("https://graph.microsoft.com/beta", scopes)
    .AddInMemoryTokenCaches();

builder.Services.AddScoped<IOnlineMeetingService, OnlineMeetingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Meeting}/{action=Index}/{id?}");

app.Run();
