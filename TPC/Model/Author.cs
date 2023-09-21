namespace Models;

public class Author : Person
{
    //Navigation Property
    public ICollection<PostModel>? Posts { get; set; }

    public Author()
    {
        Role = Role.Writer;
    }
}