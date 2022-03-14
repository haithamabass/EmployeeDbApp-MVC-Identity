
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EmployeeDbAppContextConnection");
builder.Services.AddDbContext<EmployeeDbAppContext>(options =>
    options.UseSqlServer(connectionString));
var connectionString2 = builder.Configuration.GetConnectionString("ContentConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString2));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<EmployeeDbAppUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

})
  .AddRoles<IdentityRole>()
 .AddDefaultUI()
 .AddEntityFrameworkStores<EmployeeDbAppContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();






var app = builder.Build();

using (IServiceScope? scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<EmployeeDbAppContext>();
        //should be arrnged at the same sequence in the seed class 
        var userManager = services.GetRequiredService<UserManager<EmployeeDbAppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedDefaultRolesAndUsers.SeedRoles(userManager, roleManager);
        await SeedDefaultRolesAndUsers.SeedUsers(userManager, roleManager);


        var contextDb = services.GetRequiredService<AppDbContext>();
        await SeedCitesAndDepartments.SeedCites(contextDb);
        await SeedCitesAndDepartments.SeedDepartments(contextDb);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }




}








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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();

});

app.Run();
