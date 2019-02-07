using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Account
{
    public interface IAccount<TAccountKey>
    {
        TAccountKey AccountNumber { get; set; }
        State State { get; set; }
    }
}
