namespace Domain.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<ResultEntity>
{
    public void Configure(EntityTypeBuilder<ResultEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Quiz)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.QuizId);
        builder
            .HasMany(x => x.ResultDetails)
            .WithOne(x => x.Result);
    }
}
