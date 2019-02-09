using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bankGoMyCode.Transaction;



namespace bankGoMyCode.Account
{
    public abstract class AbstractAccount<TClientKey  , TTransaction , TAccountKey , TTransactionKey> : IAccount<TAccountKey, TTransaction , TTransactionKey> 
        where TTransaction : Transaction<TTransactionKey , TAccountKey>  , new()
    {
        public double Balance { get; set; }
        public TClientKey Owner { get; set; }
        public DateTime Date { get; set; }
        public TAccountKey AccountNumber { get ; set ; }
        public State State { get ; set ; }
        public double TaxRatio { get; set; }
        private List<TTransaction> transactions { get; set; }

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

        
        public virtual void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Outgoing, Transaction.State.Ready, transactionNumber, AccountNumber, targetKey, amount);
            LoggingTransation((TTransaction)transaction);
            if (Balance > amount)
            {
                transaction.State = Transaction.State.Accepted;
                Balance -= amount;
                SendMoney((TTransaction)transaction);
            }
            else
            {
                transaction.State = Transaction.State.Rejected;
                LoggingTransation((TTransaction)transaction);
                throw new Exception("amount too high");
            }
        }

        public virtual void Credit(double amount ,TAccountKey sourceTransactionKey , TTransactionKey transactionNumber)
        {
            if (State == State.Closed)
            {
                throw new Exception("account closed");
            }
            else
            {
                Balance += amount;
                Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Incoming, Transaction.State.Accepted, transactionNumber, sourceTransactionKey, AccountNumber, amount);
                SendMoney((TTransaction)transaction);
            }
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
            
            Console.WriteLine("logging transaction "+transaction.TransactionNumber);

        }

        public IEnumerable<TTransaction> GetTransactionsByDate(DateTime date)
        {
            return from t in transactions where t.Date.Equals(date) select t; 
        }

        
    }
}
