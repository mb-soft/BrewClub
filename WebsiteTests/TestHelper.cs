using mbsoft.BrewClub.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteTests
{
    class TestHelper
    {
        private static User testUser;

        public static User GetTestUser()
        {
            if (testUser == null)
            {
                testUser = new User() { Id = Guid.NewGuid().ToString(), UserName = "test", Email = "test@gmail.com", FullName = "Test Person" };
            }

            return testUser;
        }

        public static Mock<BrewClubDbContext> GetMockedBrewClubDBContext()
        {
            //CallBase must be true otherwise you'll get 'IdentityUserLogin' has no key defined errors. 
            var mockDbContext = new Mock<BrewClubDbContext>() { CallBase = true };

            //Create dummy user test data, have to support IQueryable mocking in order to support DBSet.First() extension method usage.
            var users = new List<User>() { GetTestUser() };
            IQueryable<User> queryableUsers = users.AsQueryable();
            var userDbSet = new Mock<System.Data.Entity.DbSet<User>>();
            userDbSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(queryableUsers.Provider);
            userDbSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(queryableUsers.Expression);
            userDbSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(queryableUsers.ElementType);
            userDbSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(queryableUsers.GetEnumerator());

            var observableUserCollection = new ObservableCollection<User>(users);
            userDbSet.Setup(x => x.Local).Returns(observableUserCollection);

            userDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(GetTestUser());

            mockDbContext.Setup(x => x.Users).Returns(userDbSet.Object);

            return mockDbContext;
        }

        public static void InitializeMockedHttpContext(bool isUserLoggedIn, Controller target)
        {
            var mockHttpContext = GetMockedHttpContext(isUserLoggedIn);
            var controllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), target);
            target.ControllerContext = controllerContext;
        }

        public static Mock<HttpContextBase> GetMockedHttpContext(bool isUserLoggedIn)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var principal = GetMockedPrinciple(isUserLoggedIn);

            context.SetupGet(c => c.Request).Returns(request.Object);
            context.SetupGet(c => c.Response).Returns(response.Object);
            context.SetupGet(c => c.Session).Returns(session.Object);
            context.SetupGet(c => c.Server).Returns(server.Object);
            context.SetupGet(c => c.User).Returns(principal.Object);

            return context;
        }

        public static Mock<IPrincipal> GetMockedPrinciple(bool isUserLoggedIn)
        {
            var mock = new Mock<IPrincipal>();

            mock.SetupGet(i => i.Identity).Returns(GetMockedIdentity(isUserLoggedIn).Object);
            mock.Setup(i => i.IsInRole(It.IsAny<string>())).Returns(false);

            return mock;
        }

        public static Mock<IIdentity> GetMockedIdentity(bool isUserLoggedIn)
        {
            var mock = new Mock<IIdentity>();

            mock.SetupGet(i => i.AuthenticationType).Returns(isUserLoggedIn ? "Mock Identity" : null);
            mock.SetupGet(i => i.IsAuthenticated).Returns(isUserLoggedIn);
            mock.SetupGet(i => i.Name).Returns(isUserLoggedIn ? GetTestUser().FullName : null);

            return mock;
        }
    }
}
