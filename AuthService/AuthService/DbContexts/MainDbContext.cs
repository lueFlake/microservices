using Afonin.AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace Afonin.AuthService.DbContexts
{

    public class MainDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MainDbContext(DbContextOptions<MainDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }

            return base.SaveChanges();
        }
    }
}