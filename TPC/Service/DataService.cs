public class DataService
{
    private readonly AppDbContext _context;

    public DataService(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit() => await _context.SaveChangesAsync();

    public async Task<Author> AddAuthor(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        _context.Entry(author).State = EntityState.Detached;
        return author;
    }

    public async Task<PublicUser> AddPublicUser(PublicUser publicuser)
    {
        await _context.PublicUsers.AddAsync(publicuser);
        await _context.SaveChangesAsync();
        _context.Entry(publicuser).State = EntityState.Detached;
        return publicuser;
    }

    public async Task<PostModel> AddPost(PostModel post)
    {
        await _context.PostModels.AddAsync(post);
        await _context.SaveChangesAsync();
        _context.Entry(post).State = EntityState.Detached;
        return post;
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        _context.Entry(comment).State = EntityState.Detached;
        return comment;
    }

    public IResult GetAllAuthors()
    {
        var authors = _context.Authors.Include("Posts").ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(authors, options);
    }

    public IResult GetAuthorByName(string name)
    {
        var author = _context.Authors.Include("Posts").FirstOrDefault(a => a.Name == name);
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(author, options);
    }

    public IResult GetAllPublicUsers()
    {
        var publicUsers = _context.PublicUsers.ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(publicUsers, options);
    }

    public IResult GetPublicUserByName(string name)
    {
        var publicUser = _context.PublicUsers.Include("Posts").FirstOrDefault(a => a.Name == name);
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(publicUser, options);
    }

    public IResult GetAllPosts()
    {
        var posts = _context.PostModels.Include("Author").Include("Comments").ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(posts, options);
    }

    public IResult GetPostsByAuthorName(string name)
    {
        var post = _context.PostModels.Include("Author").Include("Comments").Where(p => p.Author.Name == name).ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(post, options);
    }

    public IResult GetCommentsByPostId(int postId)
    {
        var comment = _context.Comments.Include("Post").Include("Person").Include("Comments").Where(c => c.PostId == postId).ToList();
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        return Results.Json(comment, options);
    }
}
