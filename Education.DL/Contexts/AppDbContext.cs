using Enums = Education.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Education.Core.Models;

namespace Education.DL.Contexts;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<News> News { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        #region Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "16ea6ead-6990-4303-8a9d-9fc34b5794fd", Name = Enums.UserRoles.Admin.ToString(), NormalizedName = Enums.UserRoles.Admin.ToString().ToUpper() },
            new IdentityRole { Id = "78c55628-3a7b-4abf-8d21-6faec813274f", Name = Enums.UserRoles.User.ToString(), NormalizedName = Enums.UserRoles.User.ToString().ToUpper() }
        );
        #endregion

        #region Admin
        IdentityUser admin = new()
        {
            Id = "a5d28b24-14bb-40d1-a6e5-39c65118eb66",
            UserName = "admin",
            NormalizedUserName = "ADMIN"
        };

        PasswordHasher<IdentityUser> hasher = new();
        admin.PasswordHash = hasher.HashPassword(admin, "admin123");

        builder.Entity<IdentityUser>().HasData(admin);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin.Id, RoleId = "16ea6ead-6990-4303-8a9d-9fc34b5794fd" });
        #endregion

        base.OnModelCreating(builder);
    }
}
