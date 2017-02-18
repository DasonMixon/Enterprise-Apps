using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureVault.Models
{
    public class UserManager
    {
        public bool IsValid(string email, string password)
        {
            using (var db = new SecureVaultDataEntities())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }
    }
}