using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Client
{
    public interface IClient<TAccountEntity>
    {
        IEnumerable<TAccountEntity> GetAllAccounts();
        TAccountEntity GetAccount(TAccountEntity accountNumber);
        void CreateAccount(TAccountEntity account);
        void CloseAccount(TAccountEntity account);
    }
}
