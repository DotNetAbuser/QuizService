
namespace Domain.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<QuizEntity>
{
    public void Configure(EntityTypeBuilder<QuizEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Results)
            .WithOne(x => x.Quiz);
        builder
            .HasMany(x => x.Questions)
            .WithOne(x => x.Quiz);
    }
}
