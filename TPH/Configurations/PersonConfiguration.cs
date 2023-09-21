namespace Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.UseTphMappingStrategy();

        builder.HasKey(e => e.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Role).IsRequired();
        builder.Property(p => p.Name).IsRequired();

    }
}