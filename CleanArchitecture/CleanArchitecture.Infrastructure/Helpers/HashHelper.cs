using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CleanArchitecture.Infrastructure.Helpers
{
    public class HashHelper
    {
        public static bool VerifyPassword(string storedHash, string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
        
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); 
        }
    }
}
