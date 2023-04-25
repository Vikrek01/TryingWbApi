using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using testWebAPI.Models;

namespace testWebAPI.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
            this.SeedRoles(builder);
        }
        private void SeedUsers(ModelBuilder builder)
        {
            var user1 = new IdentityUser()
            {
                Id = "634a60ec-96bc-4f37-937f-27ba52f58d41",
                UserName = "anu@gmail.com",
                NormalizedUserName = "ANU@GMAIL.COM",
                Email = "anu@gmail.com",
                NormalizedEmail = "ANU@GMAIL.COM"
            };
            var user2 = new IdentityUser()
            {
                Id = "c80d49e8-a97c-45e6-babf-0760a6b86814",
                UserName = "maneet@gmail.com",
                NormalizedUserName = "MANEET@GMAIL.COM",
                Email = "maneet@gmail.com",
                NormalizedEmail = "MANEET@GMAIL.COM"
            };
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Anu@123");
            user2.PasswordHash = passwordHasher.HashPassword(user2, "Maneet@123");
            builder.Entity<IdentityUser>().HasData(user1);
            builder.Entity<IdentityUser>().HasData(user2);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "b31f9c27-4c2d-4f82-9aa8-a5a10382ca98", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "fa6f6c49-37f8-4447-a6bd-1c0a2a1353dc", Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "Manager" }
        );
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "b31f9c27-4c2d-4f82-9aa8-a5a10382ca98", UserId = "634a60ec-96bc-4f37-937f-27ba52f58d41" }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fa6f6c49-37f8-4447-a6bd-1c0a2a1353dc", UserId = "c80d49e8-a97c-45e6-babf-0760a6b86814" }
                );
        }
    }
}

