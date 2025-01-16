using Enums = Klinik.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Klinik.Core.Models;

namespace Klinik.DL.Contexts;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        #region Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "5116acb5-2bd6-485b-9480-307c7fc12416", Name = Enums.Roles.Admin.ToString(), NormalizedName = Enums.Roles.Admin.ToString().ToUpper() },
            new IdentityRole { Id = "6c40cbc1-654d-4a76-b9a1-77a74d66c465", Name = Enums.Roles.User.ToString(), NormalizedName = Enums.Roles.User.ToString().ToUpper() }
        );
        #endregion

        #region Admin
        IdentityUser admin = new()
        {
            Id = "e421f4b8-f69c-4afa-90d8-ecc43086ffb9",
            UserName = "admin",
            NormalizedUserName = "ADMIN"
        };

        PasswordHasher<IdentityUser> hasher = new();
        admin.PasswordHash = hasher.HashPassword(admin, "admin123");

        builder.Entity<IdentityUser>().HasData(admin);

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = admin.Id, RoleId = "5116acb5-2bd6-485b-9480-307c7fc12416" }
        );
        #endregion

        base.OnModelCreating(builder);
    }
}
