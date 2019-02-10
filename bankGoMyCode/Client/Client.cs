using bankGoMyCode.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using bankGoMyCode.Transaction;
using System.Runtime.Serialization;
using Serilog;

namespace bankGoMyCode.Client
{
    [DataContract]
    public class Client<TTransactionKey, TAccountkey, TAccountEntity, TTransaction> : AbstractClient<TAccountEntity , TAccountkey> , IEquatable<int>
        where TAccountEntity : IAccount<TAccountkey, TTransaction,TTransactionKey>
    {
        public Client(int c , string n ) : base(c , n) { }
        
        public override void CloseAccount(TAccountEntity account)
        {
            TAccountEntity a =  (from acc in Accounts where acc.AccountNumber.Equals(account.AccountNumber)  select acc).FirstOrDefault() ;
            a.State = Account.State.Closed;
            var log = new LoggerConfiguration()
         .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
         .CreateLogger();
            log.Information(account.AccountNumber + " this account has been closed " );
        }

        
        public override void CreateAccount(TAccountEntity account)
        {
            Accounts.Add(account);
            var log = new LoggerConfiguration()
         .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
         .CreateLogger();
            log.Information("new account has beed created :" + account.AccountNumber);
        }

        public bool Equals(int other)
        {
            return Cin.Equals(other);
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
