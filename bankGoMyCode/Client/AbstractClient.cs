using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Client
{
    public abstract class AbstractClient<TAccountEntity , TAccountkey> : IClient<TAccountEntity , TAccountkey>
    {
        public int Cin { get; set; }
        public string Name { get; set; }
        public List<TAccountEntity> Accounts { get; set; } 

        public AbstractClient (int c , string name )
        {
            Cin = c;
            Name = name;
            Accounts = new List<TAccountEntity>();
        }

        public abstract void CloseAccount(TAccountEntity account);
        public abstract void CreateAccount(TAccountEntity account);
        public abstract TAccountEntity GetAccount(TAccountkey accountNumber);
        public abstract IEnumerable<TAccountEntity> GetAllAccounts();
    }
}
