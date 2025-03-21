namespace HotelManagement.WebApp.Data;

public class HotelDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Guest>().Property(o => o.Id).UseIdentityColumn();
        builder.Entity<Guest>().Property(o => o.Name).IsRequired();
        builder.Entity<Guest>().Property(o => o.Phone).IsRequired();

        builder.Entity<Room>().Property(o => o.Id).UseIdentityColumn();
        builder.Entity<Room>().Property(o => o.Number).IsRequired();
        builder.Entity<Room>().Property(o => o.Price).HasColumnType("decimal(18,2)");

        builder.Entity<Reservation>().Property(o => o.Id).UseIdentityColumn();
        builder.Entity<Reservation>().Property(o => o.StartData).IsRequired();
        builder.Entity<Reservation>().Property(o => o.EndData).IsRequired();
        builder.Entity<Reservation>().Property(o => o.Status).IsRequired();

        // Relation between tables
        builder.Entity<Reservation>()
            .HasOne(o => o.Guest)
            .WithMany(o => o.Reservations)
            .HasForeignKey(o => o.GuestId);

        builder.Entity<Reservation>()
            .HasOne(o => o.Room)
            .WithMany(o => o.Reservations)
            .HasForeignKey(o => o.RoomId);


        builder.Entity<Room>().HasData(
                                    new Room { Id = 1, Number = "101", Price = 100 }, 
                                    new Room { Id =2, Number = "102", Price = 500 });

        // init data for the Guest
        builder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "Jhon", Phone = "+985654784125" });
    }
}
