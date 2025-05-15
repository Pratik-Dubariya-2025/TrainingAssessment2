using Microsoft.EntityFrameworkCore;
using TrainingAssessment.Models.Models;

namespace TrainingAssessment.Models.Data;

public class TrainingAssessmentDbContext : DbContext
{
    public TrainingAssessmentDbContext(DbContextOptions options) : base(options)
    {}

    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id).HasName("role_pkey");
            entity.Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(r => r.Name).IsUnique();
            entity.Property(r => r.Name).HasMaxLength(250);

            entity.HasData(
                new Role { Id = 1, Name = "Admin", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "User", IsDeleted = false, CreatedAt = DateTime.UtcNow }
            );
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.IsDeleted)
                .HasDefaultValueSql("false");
            entity.HasOne(u => u.Role).WithMany(p => p.Users)
                .HasForeignKey(u => u.RoleId)
                .HasConstraintName("fk_users_role");

            entity.HasData(
                new User { Id = 1, FirstName = "Pratik", LastName = "Dubariya", Username = "Pratik.Dubariya", Email = "pratik@mailinator.com", Password = "123", RoleId = 1 },
                new User { Id = 2, FirstName = "Milan", LastName = "Gohel", Username = "Milan.Gohel", Email = "milan@mailinator.com", Password = "123", RoleId = 2 },
                new User { Id = 3, FirstName = "Jagnesh", LastName = "Tank", Username = "Jagnesh.Tank", Email = "jagnesh@mailinator.com", Password = "123", RoleId = 2 },
                new User { Id = 4, FirstName = "Jignesh", LastName = "Bambhva", Username = "Jignesh.Bambhva", Email = "jignesh@mailinator.com", Password = "123", RoleId = 2 },
                new User { Id = 5, FirstName = "Sandip", LastName = "Lakhdhariya", Username = "Sandip.Lakhdhariya", Email = "sandip@mailinator.com", Password = "123", RoleId = 2 },
                new User { Id = 6, FirstName = "Simit", LastName = "Gamdha", Username = "Simit.Gamdha", Email = "simit@mailinator.com", Password = "123", RoleId = 2 },
                new User { Id = 7, FirstName = "Dhruv", LastName = "Savsani", Username = "Dhruv.Savsani", Email = "dhruv@mailinator.com", Password = "123", RoleId = 2 }
            );
        });

        modelBuilder.Entity<Concert>(entity => {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.IsDeleted)
                .HasDefaultValueSql("false");
        });
        
        modelBuilder.Entity<BookTicket>(entity => {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.IsDeleted)
                .HasDefaultValueSql("false");
            entity.HasOne(u => u.Concert).WithMany(p => p.BookTickets)
                .HasForeignKey(u => u.ConcertId)
                .HasConstraintName("fk_bookticket_concert");
            entity.HasOne(u => u.User).WithMany(p => p.BookTickets)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("fk_bookticket_user");
        });
    }

}
