using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatsCRUD.Services;
using CatsCRUD.Services.DAL;
using CatsCRUD.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CatsCRUD.Test
{
    [TestClass]
    public class CatServiceTest
    {
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;

        private readonly Mock<ICatRepository<Cat>> _moqCatRepository;

        private readonly List<Cat> _catsNotEmpty = new List<Cat> { new Cat(), new Cat() };

        private readonly List<Cat> _catsEmpty = new List<Cat>();

        private readonly Cat _cat = new Cat {Age = 11, IsAlive = false, Name = "asdas", Id = 1};

        public CatServiceTest()
        {
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _moqCatRepository = new Mock<ICatRepository<Cat>>();

            _moqUnitOfWork.Setup(x => x.CatRepository).Returns(_moqCatRepository.Object);

            _moqUnitOfWork.Setup(x => x.SaveAsync()).Returns(Task.FromResult(new{}));
        }

        [TestMethod]
        public async Task GetAllReturnTwoCat()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.GetAsync(null, null, "")).ReturnsAsync(_catsNotEmpty);

            var c = await catService.GetAllAsync();

            _moqCatRepository.Verify(x => x.GetAsync(null, null, ""), Times.Once);

            Assert.AreEqual(_catsNotEmpty, c);
        }

        [TestMethod]
        public async Task GetCatIsNotEqualToNull()
        {

            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_cat);

            var c = await catService.GetAsync(1);

            _moqCatRepository.Verify(x => x.GetByIdAsync(1), Times.Once);

            Assert.IsNotNull(c);

        }

        [TestMethod]
        public async Task GetCatIsEqualToNull()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Cat)null);

            var c = await catService.GetAsync(1);

            _moqCatRepository.Verify(x => x.GetByIdAsync(1), Times.Once);

            Assert.IsNull(c);
        }

        [TestMethod]
        public async Task Post()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.InsertAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new{}));

            await catService.AddAsync(It.IsAny<Cat>());


            _moqCatRepository.Verify(x => x.InsertAsync(It.IsAny<Cat>()), Times.Once);
            _moqUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task PutWithoutException()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.UpdateAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new { }));

            await catService.UpdateAsync(It.IsAny<Cat>());

            _moqCatRepository.Verify(x => x.UpdateAsync(It.IsAny<Cat>()), Times.Once);
            _moqUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public async Task PutWithException()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.UpdateAsync(It.IsAny<Cat>())).Throws<DbUpdateConcurrencyException>();

            await catService.UpdateAsync(It.IsAny<Cat>());

            _moqCatRepository.Verify(x => x.UpdateAsync(It.IsAny<Cat>()), Times.Once);
            _moqUnitOfWork.Verify(x => x.SaveAsync(), Times.Never);
        }

        [TestMethod]
        public async Task DeleteWithoutException()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.DeleteAsync(1)).Returns(Task.FromResult(new { }));

            await catService.DeleteAsync(1);

            _moqCatRepository.Verify(x => x.DeleteAsync(1), Times.Once);
            _moqUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteWithException()
        {
            var catService = new CatService(_moqUnitOfWork.Object);

            _moqCatRepository.Setup(x => x.DeleteAsync(1)).Throws<ArgumentNullException>();

            await catService.DeleteAsync(1);

            _moqCatRepository.Verify(x => x.DeleteAsync(1), Times.Once);
            _moqUnitOfWork.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
