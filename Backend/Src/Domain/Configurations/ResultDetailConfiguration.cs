
namespace Domain.Configurations;

public class ResultDetailConfiguration : IEntityTypeConfiguration<ResultDetailEntity>
{
    public void Configure(EntityTypeBuilder<ResultDetailEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasOne(x => x.Result)
            .WithMany(x => x.ResultDetails)
            .HasForeignKey(x => x.ResultId);
        builder
            .HasOne(x => x.Question)
            .WithMany(x => x.ResultDetails)
            .HasForeignKey(x => x.QuestionId);
        builder
            .HasOne(x => x.Option)
            .WithMany(x => x.ResultDetails)
            .HasForeignKey(x => x.OptionId);
        
    }
}
