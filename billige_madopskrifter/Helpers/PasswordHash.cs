using System.Security.Cryptography;
using System.Text;

namespace billige_madopskrifter.Helpers
{
    // Lavet med hjælp fra projektet https://github.com/Ahaubro/Wemuda-book-app 
    public interface IPasswordHelper
    {
        (byte[] passwordHash, byte[] passwordSalt) CreateHash(string password);
        string GenerateRandomString(int length);
        bool VerifyPassword(string password, byte[] hash, byte[] salt);
    }
    public class PasswordHash : IPasswordHelper
    {
        public string GenerateRandomString(int length)
        {
            return new(Enumerable.
                Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
                .Select(x =>
                {
                    var cryptoResult = new byte[4];
                    using (var cryptoProvider = new RNGCryptoServiceProvider())
                    {
                        cryptoProvider.GetBytes(cryptoResult);
                    }

                    return x[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(x.Length)];
                }).ToArray());
        }

        public (byte[] passwordHash, byte[] passwordSalt) CreateHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using var hmac = new HMACSHA512();

            return (
                passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes((password))),
                passwordSalt: hmac.Key
            );
        }

        public bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (hash == null) return false;
            if (salt == null) return false;

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            }

            if (hash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(hash));
            }

            if (salt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(salt));
            }

            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return !computedHash.Where((t, i) => t != hash[i]).Any();
        }

    }
}
