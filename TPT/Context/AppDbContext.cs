namespace Context;

public class AppDbContext : DbContext
{
    static bool cleanedOnStart = false;
    public AppDbContext(DbContextOptions options) : base(options)
    {
        if (!cleanedOnStart)
        {
            foreach (var entity in ChangeTracker.Entries().ToArray())
            {
                if (entity.Entity != null)
                {
                    entity.State = EntityState.Detached;
                }
            }
            Database.EnsureDeletedAsync().GetAwaiter().GetResult();
            Database.EnsureCreatedAsync().GetAwaiter().GetResult();
            cleanedOnStart = true;
        }
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new PublicUserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}