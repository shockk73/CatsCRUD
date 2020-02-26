using System.Threading.Tasks;
using CatsCRUD.Services;
using CatsCRUD.Services.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CatsCRUD.Test
{
    [TestClass]
    public class IdentityServiceTest
    {

        private readonly Mock<IAuthService> _moqAuthService;

        public IdentityServiceTest()
        {
            _moqAuthService = new Mock<IAuthService>();
        }

        [TestMethod]
        public async Task TryCreateTokenIsNotEqualToNull()
        {
            var identityService = new IdentityService(_moqAuthService.Object);

            _moqAuthService.Setup(x => x.AuthAsync("123", "123")).ReturnsAsync(new User { Login = "123", Password = "123", Role = "123", Id = 1});

            var u = await identityService.TryCreateTokenAsync("123", "123");

            _moqAuthService.Verify(x => x.AuthAsync("123", "123"), Times.Once);

            Assert.IsNotNull(u);
        }

        [TestMethod]
        public async Task TryCreateTokenIsEqualToNull()
        {
            var identityService = new IdentityService(_moqAuthService.Object);

            _moqAuthService.Setup(x => x.AuthAsync("123", "123")).ReturnsAsync(((User)null));

            var u = await identityService.TryCreateTokenAsync("123", "123");

            _moqAuthService.Verify(x => x.AuthAsync("123", "123"), Times.Once);

            Assert.IsNull(u);
        }

    }
}
