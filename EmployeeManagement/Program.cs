using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using EmployeeManagement.Repository.IRepository;
using EmployeeManagement.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(config =>
{
    var police = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();
     config.Filters.Add(new AuthorizeFilter(police)); 
   
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IEmployeeRepository, SqlEmpolyeeRepImp>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    //opt.SignIn.RequireConfirmedEmail = true;
}
).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



builder.Services.Configure<IdentityOptions>(opts => {
    opts.Password.RequiredLength = 3;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequiredUniqueChars = 0;
    //opts.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new
        SymmetricSecurityKey
        (Encoding.UTF8.GetBytes
        (builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.ConfigureApplicationCookie(options =>
options.LoginPath = $"/Account/Register"

);
builder.Services.AddSession();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeletePolicy",
        policy => policy.RequireClaim("Delete Role"));

    options.AddPolicy("EditRolePolicy",
        policy => policy.Requirements.Add(new ManageAdminRolesAndClaimsRequirement()));


    //options.InvokeHandlersAfterFailure = false;



    //options.AddPolicy("EditRolePolicy",
    //    policy => policy.RequireAssertion(context =>
    //    context.User.IsInRole("Admin") &&
    //    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
    //    context.User.IsInRole("Super Admin")
    //    ));

    //options.AddPolicy("AdminRolePolicy",
    //    policy => policy.RequireRole("Admin"));
});
//builder.Services.AddSingleton<IAuthorizationHandler,
//        CanEditOnlyOtherAdminRolesAndClaimsHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientSecret = "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";
   
    // options.ClientId = "835113649798-c1cth84s4fkng0f1sphh8mnkr1phqnaf.apps.googleusercontent.com";
    options.ClientId = "835113649798-c1cth84s4fkng0f1sphh8mnkr1phqnaf.apps.googleusercontent.com";
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.Use(async (context, next) =>
    {
        var token = context.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await next();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
