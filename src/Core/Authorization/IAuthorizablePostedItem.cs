﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Authorization
{
    public interface IAuthorizablePostedItem
    {
        string PostedItemAuthorID { get; }
    }
}