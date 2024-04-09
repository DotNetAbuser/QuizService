namespace Domain.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Username)
            .IsUnique();
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.HasIndex(x => x.Phone)
            .IsUnique();

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);
        builder
            .HasMany(x => x.Results)
            .WithOne(x => x.User);
        builder
            .HasMany(x => x.RefreshSessions)
            .WithOne(x => x.User);


        var defaultUsers = new List<UserEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)Role.Student,
                LastName = "Фамилия",
                FirstName = "Имя",
                MiddleName = "Отчество",
                Username = "student",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003"),
                Email = "student@example.com",
                Phone = "+79177793601",  
                IsActive = true,
                Created = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)Role.Teacher,
                LastName = "Фамилия",
                FirstName = "Имя",
                MiddleName = "Отчество",
                Username = "teacher",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003"),
                Email = "teacher@example.com",
                Phone = "+79177793602",  
                IsActive = true,
                Created = DateTime.UtcNow 
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)Role.Admin,
                LastName = "Фамилия",
                FirstName = "Имя",
                MiddleName = "Отчество",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003"),
                Email = "admin@example.com",
                Phone = "+79177793603",  
                IsActive = true,
                Created = DateTime.UtcNow 
            }
        };

        builder.HasData(defaultUsers);
    }
}
