using System.Text;

namespace Assignment1.Services
{
    public class PasswordService
    {
        public static string encryptPassword(string pass)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(pass));
        }
    }
}