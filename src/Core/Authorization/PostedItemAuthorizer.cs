using mbsoft.BrewClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Authorization
{
    public class PostedItemAuthorizer : IPostedItemAuthorizer
    {
        public PostedItemAuthorizer()
        {

        }

        public bool IsAllowedToCreatePost(IEnumerable<string> userRoleIDs)
        {
            bool isAnonymousUser = userRoleIDs.Count() == 0;
            return isAnonymousUser == false;
        }

        public bool IsAllowedToCreatPostComment(IEnumerable<string> userRoleIDs)
        {
            bool isAnonymousUser = userRoleIDs.Count() == 0;
            return isAnonymousUser == false;
        }

        public bool IsPostedItemEditable(string userID, IEnumerable<string> userRoleIDs, IAuthorizablePostedItem item)
        {
            return HasDefaultUserAccess(userID, userRoleIDs, item.PostedItemAuthorID);
        }

        public bool IsPostedItemDeletable(string userID, IEnumerable<string> userRoleIDs, IAuthorizablePostedItem item)
        {
            return HasDefaultUserAccess(userID, userRoleIDs, item.PostedItemAuthorID);
        }

        public bool IsPostedItemCommentEditable(string userID, IEnumerable<string> userRoleIDs, IAuthorizableComment comment)
        {
            return HasDefaultUserAccess(userID, userRoleIDs, comment.CommentAuthorID);
        }

        public bool IsPostedItemCommentDeletable(string userID, IEnumerable<string> userRoleIDs, IAuthorizableComment comment)
        {
            return HasDefaultUserAccess(userID, userRoleIDs, comment.CommentAuthorID);
        }

        private bool HasDefaultUserAccess(string userID, IEnumerable<string> userRoleIDs, string authorID)
        {
            if (HasRoleOverrideAccess(userRoleIDs))
            {
                return true;
            }
            else
            {
                return authorID == userID;
            }
        }

        private bool HasRoleOverrideAccess(IEnumerable<string> userRoleIDs)
        {
            return (userRoleIDs.Contains(BrewClubRoleManager.Admin) || userRoleIDs.Contains(BrewClubRoleManager.Moderator));
        }

    }
}
