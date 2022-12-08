using billige_madopskrifter.Data;
using billige_madopskrifter.Helpers;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace billige_madopskrifter.Service
{
    public interface IUserService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
        Task<GetAllUsersResponseDto> GetAll();
        Task<GetUserByIdResponseDto> GetById(int id);
        Task<CreateUserResponseDto> Create(CreateUserRequestDto dto);
    }

    public class UserService : IUserService
    {
        private readonly DBContext _context;
        private readonly AppSettings _appSettings;
        private readonly IPasswordHelper _passwordHelper;

        //Contructor
        public UserService(IOptions<AppSettings> appSettings, DBContext context, IPasswordHelper passwordHelper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _passwordHelper = passwordHelper;
        }

        //Authenticate function
        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto dto)
        {

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(dto.Email));

            if (user == null) return new AuthenticateResponseDto { StatusText = "User not found" };

            if (!_passwordHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthenticateResponseDto { StatusText = "Incorrect password" };
            }

            // authentication successful so JWT token is generated
            var token = generateJwtToken(user);

            return new AuthenticateResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Token = token,
                StatusText = "LoggedIn",
                Email = user.Email,
             
            };
        }


        // Token generation for auth
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("name", user.FullName), new Claim("username", user.FullName) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // Get all - primært til udvikling
        public async Task<GetAllUsersResponseDto> GetAll()
        {
            var allUsers = _context.Users.ToList();

            return new GetAllUsersResponseDto
            {
                Users = allUsers.Select(b => new UserDTO
                {
                    Id = b.Id,
                    FullName = b.FullName,

                })
            };
        }

        // Get user by id
        public async Task<GetUserByIdResponseDto> GetById(int id)
        {
            var user =  await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

            if (user == null)
            {             
                return new GetUserByIdResponseDto { StatusText = "User not found" };
            } 

            

            return new GetUserByIdResponseDto
            {
                FullName = user.FullName,
                Email = user.Email,
                StatusText = "User Found"
            };
        }


        // Create new user
        public async Task<CreateUserResponseDto> Create(CreateUserRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.Email == dto.Email);

            if (user != null)
            {
                return new CreateUserResponseDto
                {
                    StatusText = "Email already in use",
                    FullName = ""
                };
            }

            var (passwordHash, passwordSalt) = _passwordHelper.CreateHash(dto.Password);

            var entity = _context.Users.Add(new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            await _context.SaveChangesAsync();

            return new CreateUserResponseDto
            {
                StatusText = "User Created",
                FullName = entity.Entity.FullName
            };

        }


    }
}
