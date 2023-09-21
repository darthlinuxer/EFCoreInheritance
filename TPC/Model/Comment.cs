namespace Models;

public sealed class Comment : Article
{
    //Foreign Keys
    public int? PostId { get; set; } //a comment might be linked to a PostId
    public int? PersonId { get; set; } //a comment always have a Person
    public int? ParentCommentId {get; set;} //a comment might be a reply of another comment

    //Navigation Properties
    public PostModel? Post { get; set; }
    public Person? Person { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public Comment? ParentComment {get; set;}

}