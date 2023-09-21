using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Datasource=app_tpt.db"));
builder.Services.AddScoped<DataService>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.MaxDepth = 3;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseRouting();
app.MapGet("/persons", (DataService dataService) => dataService.GetAllPersons());
app.MapGet("/persons/{id}", (DataService dataService, int id) => dataService.GetPersonById(id));
app.MapGet("/authors", (DataService dataService) => dataService.GetAllAuthors());
app.MapGet("/authors/{name}", (DataService dataService, string name) => dataService.GetAuthorByName(name));
app.MapGet("/articles", (DataService dataService) => dataService.GetAllArticles());
app.MapGet("/articles/{id}", (DataService dataService, int id) => dataService.GetArticleById(id));
app.MapGet("/posts", (DataService dataService) => dataService.GetAllPosts());
app.MapGet("/posts/{name}", (DataService dataService, string name) => dataService.GetPostsByAuthorName(name));
app.MapGet("/comments/{postId}", (DataService dataService, int postId) => dataService.GetCommentsByPostId(postId));

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

SeedData().GetAwaiter().GetResult();
app.Run();

async Task SeedData()
{
    using var scope = app.Services.CreateScope();
    DataService dataService = scope.ServiceProvider.GetService<DataService>()!;
    var camilo = await dataService!.AddAuthor(new Author() { Name = "Camilo" });
    var luke = await dataService!.AddPublicUser(new PublicUser() { Name = "Luke" });
    var post1 = await dataService!.AddPost(new PostModel() { AuthorId = camilo.Id, Title = "Title1", Content = "Content 1", Status = Status.draft });
    var post2 = await dataService!.AddPost(new PostModel() { AuthorId = camilo.Id, Title = "Title2", Content = "Content 2", Status = Status.published });
    var comment1 = await dataService!.AddComment(new Comment() { PostId = post1.Id, Title = "Good Article", Content = "Very good", PersonId = luke.Id });
    var comment2 = await dataService!.AddComment(new Comment() { Title = "Thanks", Content = "Really appreciate", PersonId = camilo.Id, ParentCommentId = comment1.Id });
}