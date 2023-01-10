#nullable disable
using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservations.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<BookingClass> bookingClass { get; set; }

    public DbSet<Airline> airlines { get; set; }

    public DbSet<Tickets> Tickets { get; set; }

    public DbSet<BookingDetails> bookingDetails { get; set; }

    public DbSet<BookingMaster> bookingMasters { get; set; }

    public DbSet<CityRoute> routes { get; set; }

}
