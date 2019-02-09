using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Client
{
    public interface IClient<TAccountEntity , TAccountkey>
    {
        IEnumerable<TAccountEntity> GetAllAccounts();
        TAccountEntity GetAccount(TAccountkey accountNumber);
        void CreateAccount(TAccountEntity account);
        void CloseAccount(TAccountEntity account);
    }
}
