
namespace Domain.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
{
    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.ResultDetails)
            .WithOne(x => x.Option);
        builder
            .HasOne(x => x.Question)
            .WithMany(x => x.Options);
    }
}
