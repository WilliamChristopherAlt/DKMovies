using DKMovies.DAO;
using DKMovies.BO;
using DKMovies.Data.BOs;
using DKMovies.Data.DAOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DAOs
builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<TheaterDAO>();
builder.Services.AddScoped<AuditoriumDAO>();
builder.Services.AddScoped<SeatDAO>();
builder.Services.AddScoped<MovieDAO>();
builder.Services.AddScoped<ShowTimeDAO>();
builder.Services.AddScoped<TicketDAO>();
builder.Services.AddScoped<PaymentMethodDAO>();
builder.Services.AddScoped<TicketPaymentDAO>();
builder.Services.AddScoped<EmployeeRoleDAO>();
builder.Services.AddScoped<EmployeeDAO>();
builder.Services.AddScoped<MeasurementUnitDAO>();
builder.Services.AddScoped<ConcessionDAO>();
builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<OrderItemDAO>();
builder.Services.AddScoped<OrderPaymentDAO>();
builder.Services.AddScoped<ReviewDAO>();
builder.Services.AddScoped<CountryDAO>(); // New DAO for Countries
builder.Services.AddScoped<GenreDAO>(); // New DAO for Genres
builder.Services.AddScoped<RatingDAO>(); // New DAO for Ratings
builder.Services.AddScoped<DirectorDAO>(); // New DAO for Directors
builder.Services.AddScoped<LanguageDAO>(); // New DAO for Languages

// BOs
builder.Services.AddScoped<UserBO>();
builder.Services.AddScoped<TheaterBO>();
builder.Services.AddScoped<AuditoriumBO>();
builder.Services.AddScoped<SeatBO>();
builder.Services.AddScoped<MovieBO>();
builder.Services.AddScoped<ShowTimeBO>();
builder.Services.AddScoped<TicketBO>();
builder.Services.AddScoped<PaymentMethodBO>();
builder.Services.AddScoped<TicketPaymentBO>();
builder.Services.AddScoped<EmployeeRoleBO>();
builder.Services.AddScoped<EmployeeBO>();
builder.Services.AddScoped<MeasurementUnitBO>();
builder.Services.AddScoped<ConcessionBO>();
builder.Services.AddScoped<OrderBO>();
builder.Services.AddScoped<OrderItemBO>();
builder.Services.AddScoped<OrderPaymentBO>();
builder.Services.AddScoped<ReviewBO>();
builder.Services.AddScoped<CountryBO>(); // New BO for Countries
builder.Services.AddScoped<GenreBO>(); // New BO for Genres
builder.Services.AddScoped<RatingBO>(); // New BO for Ratings
builder.Services.AddScoped<DirectorBO>(); // New BO for Directors
builder.Services.AddScoped<LanguageBO>(); // New BO for Languages


// Register ApplicationDbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
