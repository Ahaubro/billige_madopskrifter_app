using System.Text.Json.Serialization;

namespace billige_madopskrifter.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
