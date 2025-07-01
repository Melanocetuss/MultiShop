using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeaturedServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

#region Registeration

// Silinicek olan servis
//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
//    {
//        opt.LoginPath = "/Login/Index/";
//        opt.LogoutPath = "/Login/Logout/";
//        opt.AccessDeniedPath = "/Pages/AccessDenied/";
//        opt.Cookie.HttpOnly = true;
//        opt.Cookie.SameSite = SameSiteMode.Strict;
//        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//        opt.Cookie.Name = "MultiShopJwt";
//    });
// Silinicek olan servis

// Discovery
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


    #region Ocelot
    // Category Service
    builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // Product Service
    builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // ProductImage Service
    builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // ProductDetail Service
    builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // SpecialOffer Service
    builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // FeatureSlider Service
    builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
    
    // Featured Service
    builder.Services.AddHttpClient<IFeaturedService, FeaturedService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // Brand Service
    builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // About Service
    builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // Contact Service
    builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // Comment Service
    builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Comment.Path}/");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

    // Basket Service
    builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Basket.Path}/");
    }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
#endregion

#endregion

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Category}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();