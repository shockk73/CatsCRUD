using CatsCRUD.Services;
using CatsCRUD.Services.DAL;
using CatsCRUD.Services.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatsCRUD.Test
{
    [TestClass]
    public class AuthServiceTest
    {
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;

        private readonly Mock<ICatRepository<User>> _moqUserRepository;

        private readonly List<User> _usersNotEmpty = new List<User> {new User { Login = "asds", Password = "asdas"}, new User { Login = "asds", Password = "asdas" } };

        private readonly List<User> _usersEmpty = new List<User>();

        public AuthServiceTest()
        {
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _moqUserRepository = new Mock<ICatRepository<User>>();

            _moqUnitOfWork.Setup(x => x.UserRepository).Returns(_moqUserRepository.Object);

            _moqUnitOfWork.Setup(x => x.SaveAsync()).Returns((Task)null);
        }

        [TestMethod]
        public async Task GetIsNotEqualToNull()
        {
            var authService = new AuthService(_moqUnitOfWork.Object);

            _moqUserRepository.Setup(x => x.GetAsync( It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, "")).ReturnsAsync(_usersNotEmpty);

            var u = await authService.AuthAsync("123", "123");

            _moqUserRepository.Verify(x => x.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, ""), Times.Once);

            Assert.IsNotNull(u);
        }

        [TestMethod]
        public async Task GetIsEqualToNull()
        {
            var authService = new AuthService(_moqUnitOfWork.Object);

            _moqUserRepository.Setup(x => x.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, "")).ReturnsAsync(_usersEmpty);

            var u = await authService.AuthAsync("123", "123");

            _moqUserRepository.Verify(x => x.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, ""), Times.Once);

            Assert.IsNull(u);
        }
    }
}