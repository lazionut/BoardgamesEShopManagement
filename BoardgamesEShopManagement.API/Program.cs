using MediatR;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Infrastructure;
using BoardgamesEShopManagement.Infrastructure.Repositories;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.API.Profiles;
using BoardgamesEShopManagement.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBoardgameRepository, BoardgameRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMediatR(typeof(ICategoryRepository));
builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMyMiddleware();

app.MapControllers();

app.Run();
