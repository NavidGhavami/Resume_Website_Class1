using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Implementation.User;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Context;
using Resume.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



#region ConfigDatabase

var connectionString = builder.Configuration.GetConnectionString("ResumeWebsite_1");

builder.Services.AddDbContext<DatabaseContext>(option =>
	option.UseSqlServer(connectionString), ServiceLifetime.Transient);


#endregion


#region General Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUserService, UserService>();

#endregion


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
