using Microsoft.EntityFrameworkCore;

namespace LoginRegTest.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}

