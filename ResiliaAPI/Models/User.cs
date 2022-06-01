namespace ResiliaAPI.Models
{
    public class User
    {
        public User(string name, string email)
        {
            Id = Random.Shared.Next();
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
