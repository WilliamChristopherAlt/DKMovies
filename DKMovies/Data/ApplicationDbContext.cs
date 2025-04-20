using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<ShowTime> ShowTimes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<TicketPayment> TicketPayments { get; set; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
    public DbSet<Concession> Concessions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderPayment> OrderPayments { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Table names
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Theater>().ToTable("Theaters");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<EmployeeRole>().ToTable("EmployeeRoles");
        modelBuilder.Entity<Auditorium>().ToTable("Auditoriums");
        modelBuilder.Entity<Seat>().ToTable("Seats");
        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<ShowTime>().ToTable("ShowTimes");
        modelBuilder.Entity<Ticket>().ToTable("Tickets");
        modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
        modelBuilder.Entity<TicketPayment>().ToTable("TicketPayments");
        modelBuilder.Entity<MeasurementUnit>().ToTable("MeasurementUnits");
        modelBuilder.Entity<Concession>().ToTable("Concessions");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        modelBuilder.Entity<OrderPayment>().ToTable("OrderPayments");
        modelBuilder.Entity<Review>().ToTable("Reviews");

        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Genre>().ToTable("Genres");
        modelBuilder.Entity<Rating>().ToTable("Ratings");
        modelBuilder.Entity<Director>().ToTable("Directors");
        modelBuilder.Entity<Language>().ToTable("Languages");

        // Foreign keys and relationships
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShowTime>()
            .HasOne(st => st.Movie)
            .WithMany(m => m.ShowTimes)
            .HasForeignKey(st => st.MovieID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShowTime>()
            .HasOne(st => st.Auditorium)
            .WithMany(a => a.ShowTimes)
            .HasForeignKey(st => st.AuditoriumID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ShowTime>()
            .HasOne(st => st.SubtitleLanguage)
            .WithMany(a => a.Showtimes)
            .HasForeignKey(st => st.SubtitleLanguageID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Theater)
            .WithMany(t => t.Employees)
            .HasForeignKey(e => e.TheaterID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Theater>()
            .HasOne(t => t.Manager)
            .WithOne()
            .HasForeignKey<Theater>(t => t.ManagerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Auditorium>()
            .HasOne(a => a.Theater)
            .WithMany(t => t.Auditoriums)
            .HasForeignKey(a => a.TheaterID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Auditorium)
            .WithMany(a => a.Seats)
            .HasForeignKey(s => s.AuditoriumID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.ShowTime)
            .WithMany(st => st.Tickets)
            .HasForeignKey(t => t.ShowTimeID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.SeatID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TicketPayment>()
            .HasOne(tp => tp.Ticket)
            .WithMany(t => t.TicketPayments)
            .HasForeignKey(tp => tp.TicketID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TicketPayment>()
            .HasOne(tp => tp.PaymentMethod)
            .WithMany(pm => pm.TicketPayments)
            .HasForeignKey(tp => tp.MethodID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Concession>()
            .HasOne(c => c.MeasurementUnit)
            .WithMany(mu => mu.Concessions)
            .HasForeignKey(c => c.UnitID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Concession)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(oi => oi.ConcessionID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderPayment>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderPayments)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderPayment>()
            .HasOne(op => op.PaymentMethod)
            .WithMany(pm => pm.OrderPayments)
            .HasForeignKey(op => op.MethodID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Director>()
            .HasOne(d => d.Country)
            .WithMany(c => c.Directors)
            .HasForeignKey(d => d.CountryID)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.GenreID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Rating)
            .WithMany(r => r.Movies)
            .HasForeignKey(m => m.RatingID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Country)
            .WithMany(c => c.Movies)
            .HasForeignKey(m => m.CountryID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(d => d.Movies)
            .HasForeignKey(m => m.DirectorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Language)
            .WithMany(l => l.Movies)
            .HasForeignKey(m => m.LanguageID)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique constraints
        modelBuilder.Entity<Seat>()
            .HasIndex(s => new { s.AuditoriumID, s.RowLabel, s.SeatNumber })
            .IsUnique();

        modelBuilder.Entity<Ticket>()
            .HasIndex(t => new { t.SeatID, t.ShowTimeID })
            .IsUnique();

        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.GenreName)
            .IsUnique();

        modelBuilder.Entity<Rating>()
            .HasIndex(r => r.RatingValue)
            .IsUnique();

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.CountryName)
            .IsUnique();

        // Enum conversions and default values
        modelBuilder.Entity<TicketPayment>()
            .Property(tp => tp.PaymentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<OrderPayment>()
            .Property(op => op.PaymentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Review>()
            .Property(r => r.IsApproved)
            .HasDefaultValue(false);

        modelBuilder.Entity<ShowTime>()
            .Property(st => st.Is3D)
            .HasDefaultValue(false);

        modelBuilder.Entity<Concession>()
            .Property(c => c.IsAvailable)
            .HasDefaultValue(true);

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderStatus)
            .HasDefaultValue("Pending");

        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Ticket>()
            .Property(t => t.PurchaseTime)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderTime)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Employee>()
            .Property(e => e.HireDate)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Review>()
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}
