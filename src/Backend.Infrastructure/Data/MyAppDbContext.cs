using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
    public class Sample01DbContext : DbContext
    {
        public Sample01DbContext(DbContextOptions<Sample01DbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
