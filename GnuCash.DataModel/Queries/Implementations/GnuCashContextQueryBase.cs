using GnuCash.DataModel.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.DataModel.Queries.Implementations
{
    abstract class GnuCashContextQueryBase
    {
        protected readonly GnuCashContext Context;
        protected GnuCashContextQueryBase(GnuCashContext context)
        {
            Context = context;
        }
    }
}
