using System.Linq;
using System.Threading.Tasks;
using CatsCRUD.Services.DAL;
using CatsCRUD.Services.Entites;


namespace CatsCRUD.Services
{
    public class AuthService : IAuthService
    {
        readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> AuthAsync(string username, string password)
        {
            var users = (await _unitOfWork.UserRepository.GetAsync(filter: x => x.Login == username && x.Password == password, null, ""))
                .ToList();
            return users.Count > 0 ? users[0] : null;
        }
    }
}
