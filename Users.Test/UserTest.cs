using Microsoft.VisualStudio.TestTools.UnitTesting;
using Users.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.ObjectValue;
using System.Threading.Tasks;
using System.Linq;

namespace Users.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public async Task IncludeUser()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var response = await service.Save(user);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task IncludeUserWithoutEmail()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Name = "Teste", Password = "teste" };

            var response = await service.Save(user);
            Assert.IsTrue(!response.Success);
        }

        [TestMethod]
        public async Task UpdateUser()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var userSaved = await service.Save(user);

            user.Name = "Maria";
            user.OldPassword = "teste";
            var response = await service.Update(user, userSaved.Data.Guid);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task UpdateUserWithoutOldPassword()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var userSaved = await service.Save(user);

            user.Name = "Maria";
            var response = await service.Update(user, userSaved.Data.Guid);

            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task GetAllUsers()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var response = await service.GetAll();
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task GetAllUsersWithInsert()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var userSaved = await service.Save(user);

            var response = await service.GetAll();
            Assert.IsTrue(response.Success && response != null && response.Data.Any());
        }

        [TestMethod]
        public async Task GetUser()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var userSaved = await service.Save(user);

            var response = await service.Get(userSaved.Data.Guid);
            Assert.IsTrue(response.Success && response != null);
        }

        [TestMethod]
        public async Task DeleteUser()
        {
            var container = Container.RegisterServices();
            var service = container.GetService<IUserService>();

            var user = new UserRequest() { Email = "teste@teste.com.br", Name = "Teste", Password = "teste" };

            var userSaved = await service.Save(user);

            Assert.IsTrue(userSaved.Success);

            var response = await service.Delete(userSaved.Data.Guid);
            var item = await service.Get(userSaved.Data.Guid);

            Assert.IsTrue(response.Success && !item.Success);
        }
    }
}
