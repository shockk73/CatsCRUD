using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CatsCRUD.Services.Models
{
    public class AuthOptions
    {
        public const string Issuer = "Dmitry";
        public const string Audience = "CatClient"; 
        private const string Key = "a!1111111188888888888888888888888888888888888888888";   
        public const int LifeTime = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
