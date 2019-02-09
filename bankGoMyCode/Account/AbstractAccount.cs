using System;
using System.Collections.Generic;
using System.Text;
using bankGoMyCode.Transaction;
namespace bankGoMyCode.Account
{
    public abstract class AbstractAccount<TClientKey  , TTransaction , TAccountKey , TTransactionKey> : IAccount<TAccountKey, TTransaction , TTransactionKey>
        where TTransaction : Transaction<TTransactionKey , TAccountKey> , new()
    {
        public double Balance { get; set; }
        public TClientKey Owner { get; set; }
        public List<TTransaction> transactions { get; set; }
        public DateTime Date { get; set; }
        public TAccountKey AccountNumber { get ; set ; }
        public State State { get ; set ; }
        public double TaxRatio { get; set; }

        public AbstractAccount(TClientKey owner , TAccountKey accountNumber)
        {
            Balance = 0;
            AccountNumber = accountNumber;
            Owner = owner;
            transactions = new List<TTransaction>();
            Date = new DateTime();
            State = State.Active;
        }



        public IEnumerable<TTransaction> GetAllTransactions()
        {
            return transactions;
        }

        public void DebitAccount(double amount, TTransaction transaction)
        {
            // code to send transaction 
            // new transation ....
            Balance = amount; 
            SendMoney(transaction);

        }
        public virtual void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey) { }

        public virtual void Credit(double amount ,TAccountKey sourceTransactionKey , TTransactionKey transactionNumber)
        {
            // credit account 
            // and create new transaction
            Balance += amount;
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Incoming, Transaction.State.Accepted, transactionNumber, sourceTransactionKey, AccountNumber, amount);
            SendMoney((TTransaction)transaction);

        }

        public void SendMoney(TTransaction transaction)
        {
            transactions.Add(transaction);
            // logging transaction
            LoggingTransation(transaction);
        }
        public void LoggingTransation (TTransaction transaction)
        {
            // logging transaction
            Console.WriteLine("logging transaction ... ");
        }

    }
}
