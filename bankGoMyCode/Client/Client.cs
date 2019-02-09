using bankGoMyCode.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using bankGoMyCode.Transaction;

namespace bankGoMyCode.Client
{
    public class Client<TTransactionKey, TAccountkey, TAccountEntity, TTransaction> : AbstractClient<TAccountEntity , TAccountkey> 
        where TAccountEntity : IAccount<TAccountkey, TTransaction,TTransactionKey>
    {
        
        public Client(int c , string n ) : base(c , n) { }
        
        public override void CloseAccount(TAccountEntity account)
        {
            TAccountEntity a =  (from acc in Accounts where acc.AccountNumber.Equals(account.AccountNumber)  select acc).FirstOrDefault() ;
            a.State = Account.State.Closed;
        }

        
        public override void CreateAccount(TAccountEntity account)
        {
            Accounts.Add(account);
        }

        public override TAccountEntity GetAccount(TAccountkey accountNumber)
        {
            TAccountEntity account = (
                from a in Accounts
                where accountNumber.Equals(a.AccountNumber)
                select a
                ).FirstOrDefault();

            return account;
                              
        }
        public override IEnumerable<TAccountEntity> GetAllAccounts()
        {
            return Accounts;
        }

    }
}
