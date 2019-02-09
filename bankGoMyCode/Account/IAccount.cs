using bankGoMyCode.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Account
{
    public interface IAccount<TAccountKey , TTransaction , TTransactionKey>
    {
        TAccountKey AccountNumber { get; set; }
        State State { get; set; }
        IEnumerable<TTransaction> GetAllTransactions();
        void DebitAccount(double amount, TTransaction transaction);
        void Credit(double amount, TAccountKey sourceTransactionKey, TTransactionKey transactionNumber);
        void SendMoney(TTransaction transaction);

    }
}
