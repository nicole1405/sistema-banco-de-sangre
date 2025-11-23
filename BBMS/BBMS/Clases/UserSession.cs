using System;

namespace BBMS.Clases
{
    public class UserSession
    {
        // EmpId ahora es INT (PK)
        public int EmpId { get; set; }
        public string Role { get; set; }
        public string EmpName { get; set; }

        // Sesión actual (null si no hay sesión)
        public static UserSession Current { get; set; }
    }
}