public class User
{
    public string username;
    public string password;
    public string email;
    public string name;
    public int age;
    public string gender;

    public User(string username, string password, string email, string name, int age)
    {
        this.username = username;
        this.password = password;
        this.email = email;
        this.name = name;
        this.age = age;
    }
}