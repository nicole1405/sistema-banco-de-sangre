using System;
using System;
using System.Security.Cryptography;
using System.Text;


namespace BBMS.Clases
{
    // 1. Estructura para almacenar el resultado del hash (no se usa en la versión actual, pero útil si se extiende).
    public class UserAuthResult
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
    }

    // 2. Servicio estático para operaciones de autenticación de usuario.
    public static class UserAuthService
    {
        // 3. Salt fijo para el hash de contraseñas. En producción, usar salt único por usuario.
        private static readonly byte[] FixedSalt = Encoding.UTF8.GetBytes("BBMS_SALT_2025");

        // 4. Genera el hash de la contraseña usando PBKDF2 y el salt fijo.
        public static string HashPassword(string password)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, FixedSalt, 100_000, HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        // 5. Verifica si la contraseña ingresada, hasheada, coincide con el hash almacenado.
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
