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
        IEnumerable<TTransaction> GetTransactionsByDate(DateTime date);
        IEnumerable<TTransaction> GetTransactionsByTarget(TAccountKey targetAccountNumber);
        IEnumerable<TTransaction> GetTransactionsByQuery(Func<TTransaction , bool> query);

        void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey);
        void Credit(double amount, TAccountKey sourceTransactionKey, TTransactionKey transactionNumber);
        void SendMoney(TTransaction transaction);

    }
}
