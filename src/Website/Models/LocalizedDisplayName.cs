using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace mbsoft.BrewClub.Website.Models
{
    public class LocalizedDisplayName : DisplayNameAttribute
    {
        private string localizedKey;

        public LocalizedDisplayName(string localizedKey)
        {
            this.localizedKey = localizedKey;
        }

        public override string DisplayName
        {
            get
            {
                return new SiteLocalizerFactory().GetXmlStringSiteLocalizer().Localize(localizedKey);
            }
        }
    }
}