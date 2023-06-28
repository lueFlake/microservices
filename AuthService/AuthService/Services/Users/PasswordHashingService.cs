using System.Security.Cryptography;
using Afonin.AuthService.Domain.Interfaces;

namespace AdAstra.HRPlatform.API.Services.Users
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 10000;

        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = GenerateHash(password, salt);

            // Combine the salt and hash and convert to Base64 string
            byte[] combinedBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, combinedBytes, 0, SaltSize);
            Array.Copy(hash, 0, combinedBytes, SaltSize, HashSize);
            string hashedPassword = Convert.ToBase64String(combinedBytes);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Convert the hashed password back to bytes
            byte[] combinedBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            byte[] hash = new byte[HashSize];
            Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
            Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

            // Compute the hash of the entered password with the stored salt
            byte[] computedHash = GenerateHash(password, salt);

            // Compare the computed hash with the stored hash
            return SlowEquals(hash, computedHash);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
