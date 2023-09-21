namespace Models;

public class PublicUser : Person
{
    public PublicUser()
    {
        Role = Role.Public;
    }
}