using CursoOnline.Dominio._Base;
using CursoOnline.Ioc;
using CursoOnline.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(config =>
{
    config.Filters.Add(typeof(CustomExceptionFilter));
});

StartupIoc.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next.Invoke();

    var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork))!;
    await unitOfWork.Commit();
});

app.UseDeveloperExceptionPage();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
