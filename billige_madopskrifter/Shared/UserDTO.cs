using System.ComponentModel.DataAnnotations;

namespace billige_madopskrifter.Shared
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    //Get all response object
    public class GetAllUsersResponseDto
    {
        public IEnumerable<UserDTO> Users { get; set; }
    }

    // Get by id reponse object
    public class GetUserByIdResponseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StatusText { get; set; }
    }

    //Auth request opbject
    public class AuthenticateRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    //Auth response object
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string StatusText { get; set; }
        public string Email { get; set; }
    }

    // Create request object
    public class CreateUserRequestDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    // Create response object
    public class CreateUserResponseDto
    {
        public string FullName { get; set; }
        public string StatusText { get; set; }
    }
}
