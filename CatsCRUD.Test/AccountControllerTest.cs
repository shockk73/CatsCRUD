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
    public class AccountControllerTest
    {
        private readonly Mock<IIdentityService> _moqIdentityService;

        public AccountControllerTest()
        {
            _moqIdentityService = new Mock<IIdentityService>();
        }

        [TestMethod]
        public async Task GetTokenSuccess()
        {
            var accountController = new AccountController(_moqIdentityService.Object);

            _moqIdentityService.Setup(x => x.TryCreateTokenAsync("123", "123")).ReturnsAsync("string");

            var result = await accountController.Token(new TokenRequest { Username = "123", Password = "123"});

            _moqIdentityService.Verify(x => x.TryCreateTokenAsync("123", "123"), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(JsonResult));
        }

        [TestMethod]
        public async Task GetTokenError()
        {
            var accountController = new AccountController(_moqIdentityService.Object);

            _moqIdentityService.Setup(x => x.TryCreateTokenAsync("123", "123")).ReturnsAsync((string)null);

            var result = await accountController.Token(new TokenRequest { Username = "123", Password = "123" });

            _moqIdentityService.Verify(x => x.TryCreateTokenAsync("123", "123"), Times.Once);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
    }
}