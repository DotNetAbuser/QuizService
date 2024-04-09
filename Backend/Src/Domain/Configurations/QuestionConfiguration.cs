
namespace Domain.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
{
    public void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Quiz)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.QuizId);
        builder
            .HasMany(x => x.ResultDetails)
            .WithOne(x => x.Question);
        builder
            .HasMany(x => x.Options)
            .WithOne(x => x.Question);
    }
}
