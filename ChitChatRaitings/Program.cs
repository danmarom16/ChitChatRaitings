using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChitChatRaitings.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChitChatRaitingsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChitChatRaitingsContext") ?? throw new InvalidOperationException("Connection string 'ChitChatRaitingsContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Feedbacks}/{action=Search}/{id?}");

app.Run();
