using mbsoft.BrewClub.Data;
using Moq;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteTests
{
    class TestHelper
    {
        public static User GetTestUser()
        {
            return new User() { Id = Guid.NewGuid().ToString(), UserName = "test", Email = "test@gmail.com", FullName = "Test Person" };            
        }

        public static Mock<BrewClubDbContext> GetMockedBrewClubDBContext()
        {
            //CallBase must be true otherwise you'll get 'IdentityUserLogin' has no key defined errors. 
            var mockDbContext = new Mock<BrewClubDbContext>() { CallBase = true };

            var userProfilesDbSet = new Mock<System.Data.Entity.DbSet<User>>();
            var dummyUser = new User() { Id = Guid.NewGuid().ToString(), UserName = "test", Email = "test@gmail.com", FullName = "Test Person" };
            userProfilesDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(dummyUser);

            mockDbContext.Setup(x => x.Users).Returns(userProfilesDbSet.Object);

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
