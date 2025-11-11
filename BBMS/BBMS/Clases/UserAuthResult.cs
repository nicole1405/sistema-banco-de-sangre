using System;
using System;
using System.Security.Cryptography;
using System.Text;


namespace BBMS.Clases
{
    public class UserAuthResult
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
        
    }

    public static class UserAuthService
    {
        private static readonly byte[] FixedSalt = Encoding.UTF8.GetBytes("BBMS_SALT_2025");

        public static string HashPassword(string password)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, 100_000, HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, 100_000, HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(32);
                var hashBase64 = Convert.ToBase64String(hash);
                return hashBase64 == storedHash;
            }
        }
    }
}
