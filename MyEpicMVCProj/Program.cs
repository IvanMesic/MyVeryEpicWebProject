using Common.DALModels;
using Common.Interfaces;
using Common.Mapping;
using Common.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyEpicMVCProj.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RwaMoviesContext>();
builder.Services.AddDbContext<RwaMoviesContext>(options =>
{
    options.UseSqlServer("Name=ConnectionStrings:EpicConnectionString");
});

builder.Services.AddScoped<IUserReposi, UserReposi>();
builder.Services.AddScoped<IVideoRepo, VideoRepository>();
//add scoped for tag repo
builder.Services.AddScoped<ITagRepository, TagRepository>();
//scoped for Country repo
builder.Services.AddScoped<ICountryRepo, CountryRepository>();


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAutoMapper(typeof(MyEpicMVCProj.Mapping.AutomapperProfile),
                               typeof(Common.Mapping.AutomapperProfile));


builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();


/*
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
*/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=VideoMvc}/{action=Index}/{id?}");

app.Run();
