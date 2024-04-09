
namespace Domain.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
{
    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.ResultDetail)
            .WithOne(x => x.Option);
        builder
            .HasOne(x => x.Question)
            .WithMany(x => x.Options);
    }
}
