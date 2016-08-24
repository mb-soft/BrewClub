using mbsoft.BrewClub.Authorization;
using mbsoft.BrewClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Moq;

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

        [Theory]
        [MemberData("anonymousRoleIDs")]
        public void IsAllowedToCreatPostComment_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;
            bool actual = target.IsAllowedToCreatPostComment(roleIDs);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("memberRoleID")]
        [MemberData("moderatorRoleID")]
        [MemberData("userRoldID")]
        public void IsAllowedToCreatPostComment_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            bool actual = target.IsAllowedToCreatPostComment(roleIDs);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("moderatorRoleID")]
        public void IsPostedItemCommentDeletable_UserRoleOverrides_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            string userID = string.Empty;
            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns(userID);
            bool actual = target.IsPostedItemCommentDeletable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemCommentDeletable_NonUserRoleOverride_UserIDMatchesCommentAuthorID_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;

            string userID = "1";
            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns(userID);

            bool actual = target.IsPostedItemCommentDeletable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemCommentDeletable_NonUserRoleOverride_UserIDDoesNotMatchCommentAuthorID_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;

            string userID = "1";

            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns("2");

            bool actual = target.IsPostedItemCommentDeletable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("moderatorRoleID")]
        public void IsPostedItemCommentEditable_UserRoleOverrides_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            string userID = string.Empty;
            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns(userID);
            bool actual = target.IsPostedItemCommentEditable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemCommentEditable_NonUserRoleOverride_UserIDMatchesCommentAuthorID_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;

            string userID = "1";
            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns(userID);

            bool actual = target.IsPostedItemCommentEditable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemCommentEditable_NonUserRoleOverride_UserIDDoesNotMatchCommentAuthorID_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;

            string userID = "1";

            var comment = new Mock<IAuthorizableComment>();
            comment.Setup(x => x.CommentAuthorID).Returns("2");

            bool actual = target.IsPostedItemCommentEditable(userID, roleIDs, comment.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("moderatorRoleID")]
        public void IsPostedItemDeletable_UserRoleOverrides_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            string userID = string.Empty;
            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns(userID);
            bool actual = target.IsPostedItemDeletable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemDeletable_NonUserRoleOverride_UserIDMatchesPostedItemAuthorID_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;

            string userID = "1";
            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns(userID);

            bool actual = target.IsPostedItemDeletable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemDeletable_NonUserRoleOverride_UserIDDoesNotMatchPostedItemAuthorID_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;

            string userID = "1";

            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns("2");

            bool actual = target.IsPostedItemDeletable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("adminRoleID")]
        [MemberData("moderatorRoleID")]
        public void IsPostedItemEditable_UserRoleOverrides_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;
            string userID = string.Empty;
            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns(userID);
            bool actual = target.IsPostedItemEditable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemEditable_NonUserRoleOverride_UserIDMatchesPostedItemAuthorID_True(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = true;

            string userID = "1";
            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns(userID);

            bool actual = target.IsPostedItemEditable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("memberRoleID")]
        [MemberData("userRoldID")]
        public void IsPostedItemEditable_NonUserRoleOverride_UserIDDoesNotMatchPostedItemAuthorID_False(IEnumerable<string> roleIDs)
        {
            var target = GetTarget();
            bool expected = false;

            string userID = "1";

            var item = new Mock<IAuthorizablePostedItem>();
            item.Setup(x => x.PostedItemAuthorID).Returns("2");

            bool actual = target.IsPostedItemEditable(userID, roleIDs, item.Object);

            Assert.Equal(expected, actual);
        }
    }
}
