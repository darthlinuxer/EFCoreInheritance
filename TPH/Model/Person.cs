namespace Models;

//All Users can post comments
public abstract class Person
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public Role Role { get; init; } = Role.Public;

    //Navigation Properties
    public ICollection<Comment>? Comments { get; set; }
}