public class DataService
{
    private readonly AppDbContext _context;

    public DataService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Author> AddAuthor(Author author)
    {
        await _context.Persons.AddAsync(author);
        _context.SaveChanges();
        return author;
    }

    public async Task<PublicUser> AddPublicUser(PublicUser publicuser)
    {
        await _context.Persons.AddAsync(publicuser);
        _context.SaveChanges();
        return publicuser;
    }

    public async Task<PostModel> AddPost(PostModel post)
    {
        await _context.Articles.AddAsync(post);
        _context.SaveChanges();
        return post;
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        await _context.Articles.AddAsync(comment);
        _context.SaveChanges();
        return comment;
    }

    public IResult GetAllPersons()
    {
        var persons = _context.Persons.Include("Comments").ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(persons, options);
    }

    public IResult GetAllAuthors()
    {
        var authors = _context.Persons.OfType<Author>().Include("Posts").ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(authors, options);
    }

    public IResult GetPersonById(int id)
    {
        var person = _context.Persons.FirstOrDefault(p => p.Id == id);
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(person, options);
    }

    public IResult GetAuthorByName(string name)
    {
        var author = _context.Persons.OfType<Author>().Include("Posts").FirstOrDefault(a => a.Name == name);
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(author, options);
    }

    public IResult GetAllArticles()
    {
        var articles = _context.Articles.ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(articles, options);
    }

    public IResult GetAllPosts()
    {
        var posts = _context.Articles.OfType<PostModel>().Include("Author").Include("Comments").ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(posts, options);
    }

    public IResult GetArticleById(int id)
    {
        var article = _context.Articles.FirstOrDefault(a => a.Id == id);
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(article, options);
    }

    public IResult GetPostsByAuthorName(string name)
    {
        var post = _context.Articles.OfType<PostModel>().Include("Author").Include("Comments").Where(p => p.Author.Name == name).ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(post, options);
    }

    public IResult GetCommentsByPostId(int postId)
    {
        var comment = _context.Articles.OfType<Comment>().Include("Post").Include("Person").Include("Comments").Where(c => c.PostId == postId).ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(comment, options);
    }
}
