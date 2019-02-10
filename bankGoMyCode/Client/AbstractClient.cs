using bankGoMyCode.Account;
using bankGoMyCode.Transaction;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace bankGoMyCode.Client
{

    [DataContract]
    [KnownType(typeof(Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>))]

    public abstract class AbstractClient<TAccountEntity , TAccountkey> : IClient<TAccountEntity , TAccountkey>
    {
        [DataMember]
        public int Cin { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
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
