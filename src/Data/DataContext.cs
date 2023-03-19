using Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<Artist> Artists {get; set; } = default!;
        public DbSet<Ticket> Tickets {get; set; } = default!;
        public DbSet<Show> Shows {get; set; } = default!;
        public DbSet<User> Users {get; set; } = default!;
        
    }
}