namespace Models;

public sealed class PostModel: Article
{
    public int? AuthorId { get; set; }	
	public Author Author {get; set;}
	public ICollection<Comment>? Comments {get; set;}

}