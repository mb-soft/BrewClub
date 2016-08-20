using mbsoft.BrewClub.Authorization;
using mbsoft.BrewClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace CoreTests.Authorization
{
    public class PostedItemAuthorizerTests
    {
        public static IEnumerable<object[]> anonymousRoleIDs = new[] { new object[] { new List<string>() } };
        public static IEnumerable<object[]> adminRoleID = new[] { new object[] { new List<string>() { BrewClubRoleManager.Admin } } };
        public static IEnumerable<object[]> memberRoleID = new[] { new object[] { new List<string>() { BrewClubRoleManager.Member } } };
        public static IEnumerable<object[]> moderatorRoleID = new[] { new object[] { new List<string>() { BrewClubRoleManager.Moderator } } };
        public static IEnumerable<object[]> userRoldID = new[] { new object[] { new List<string>() { BrewClubRoleManager.User } } };

        private PostedItemAuthorizer GetTarget()
        {
            return new PostedItemAuthorizer();
        }

        [Theory]
        [MemberData("anonymousRoleIDs")]
        public void IsAllowedToCreatePost_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;
            bool actual = target.IsAllowedToCreatePost(roleIDs);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("memberRoleID")]
        [MemberData("moderatorRoleID")]
        [MemberData("userRoldID")]
        public void IsAllowedToCreatePost_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            bool actual = target.IsAllowedToCreatePost(roleIDs);

            Assert.Equal(expected, actual);
        }

    }
}
