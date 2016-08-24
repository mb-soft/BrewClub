using mbsoft.BrewClub.Data;
using System.Collections.Generic;

namespace mbsoft.BrewClub.Authorization
{
    public interface IPostedItemAuthorizer
    {
        bool IsPostedItemCommentDeletable(string userID, IEnumerable<string> userRoleIDs, IAuthorizableComment comment);
        bool IsPostedItemCommentEditable(string userID, IEnumerable<string> userRoleIDs, IAuthorizableComment comment);
        bool IsPostedItemDeletable(string userID, IEnumerable<string> userRoleIDs, IAuthorizablePostedItem item);
        bool IsPostedItemEditable(string userID, IEnumerable<string> userRoleIDs, IAuthorizablePostedItem item);
        bool IsAllowedToCreatePost(IEnumerable<string> userRoleIDs);
        bool IsAllowedToCreatPostComment(IEnumerable<string> userRoleIDs);
    }
}