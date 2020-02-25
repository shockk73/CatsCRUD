using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatsCRUD.Controllers;
using CatsCRUD.Models;
using CatsCRUD.Services;
using CatsCRUD.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CatsCRUD.Test
{
    [TestClass]
    public class CatsControllerTest
    {

        private readonly Mock<IMapper> moqMapper;

        private readonly Mock<ICatService> moqCatService;

        private readonly  List<Cat> cats = new List<Cat> { new Cat(), new Cat() };

        public CatsControllerTest()
        {
            moqMapper = new Mock<IMapper>();

            moqCatService = new Mock<ICatService>();

            moqMapper.Setup(x => x.Map<IEnumerable<Cat>, IEnumerable<CatResponse>>(It.IsAny<IEnumerable<Cat>>()))
                .Returns(cats.Select( c => new CatResponse() ));

            moqMapper.Setup(x => x.Map<CatRequest, Cat>(It.IsAny<CatRequest>())).Returns(It.IsAny<Cat>());
            moqMapper.Setup(x => x.Map<Cat, CatResponse>(It.IsAny<Cat>())).Returns(It.IsAny<CatResponse>());
        }

        [TestMethod]
        public async Task GetAllAreNotEqualToNull()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.GetAllAsync()).ReturnsAsync(cats);

            var result = await catsConroller.Get();

            moqCatService.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public async Task GetCatIsNotEqualToNull()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.GetAsync(1)).ReturnsAsync(cats[0]);

            var result = await catsConroller.Get(1);

            moqMapper.Verify( x => x.Map<Cat, CatResponse>(It.IsAny<Cat>()), Times.Once);

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public async Task GetCatIsEqualToNull()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.GetAsync(1)).ReturnsAsync((Cat)null);

            var result = await catsConroller.Get(1);

            moqMapper.Verify(x => x.Map<Cat, CatResponse>(It.IsAny<Cat>()), Times.Never);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostCatModelIsValid()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.AddAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new { }));

            var result = await catsConroller.Post(new CatRequest { Age = 31, IsAlive = false, Name = "yes"});

            moqCatService.Verify(x => x.AddAsync(It.IsAny<Cat>()), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PostCatModelIsNotValid()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.AddAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new { }));

            var result = await catsConroller.Post( null );

            moqCatService.Verify(x => x.AddAsync(It.IsAny<Cat>()), Times.Never);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutCatModelIsValid()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.UpdateAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new { }));

            var result = await catsConroller.Put(new CatRequest { Age = 31, IsAlive = false, Name = "yes" });

            moqCatService.Verify(x => x.UpdateAsync(It.IsAny<Cat>()), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PutCatModelIsNotValid()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.UpdateAsync(It.IsAny<Cat>())).Returns(Task.FromResult(new { }));

            var result = await catsConroller.Put(null);

            moqCatService.Verify(x => x.UpdateAsync(It.IsAny<Cat>()), Times.Never);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public async Task PutCatModelWithException()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.UpdateAsync(It.IsAny<Cat>())).Throws<DbUpdateConcurrencyException>();

            var result = await catsConroller.Put(new CatRequest { Age = 31, IsAlive = false, Name = "yes" });

            moqCatService.Verify(x => x.UpdateAsync(It.IsAny<Cat>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCatModelWithoutException()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.DeleteAsync(1)).Returns(Task.FromResult(new { }));

            var result = await catsConroller.Delete(1);

            moqCatService.Verify(x => x.DeleteAsync(1), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteCatModelWithException()
        {
            var catsConroller = new CatsController(moqCatService.Object, moqMapper.Object);

            moqCatService.Setup(x => x.DeleteAsync(1)).Throws<ArgumentNullException>();

            var result = await catsConroller.Delete(1);

            moqCatService.Verify(x => x.DeleteAsync(1), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
