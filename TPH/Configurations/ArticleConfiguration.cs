namespace Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
       public void Configure(EntityTypeBuilder<Article> builder)
       {
              builder.UseTphMappingStrategy();

              builder.HasKey(e => e.Id);
              builder.Property(p => p.Id).ValueGeneratedOnAdd();

              builder.Property(p => p.Title).HasMaxLength(255).IsRequired();
              builder.Property(p => p.Content).IsRequired();
              builder.Property(p => p.Status).IsRequired();
              builder.Property(p => p.DatePublished).IsRequired();

       }

}