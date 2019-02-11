using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using bankGoMyCode.Transaction;
using Serilog;



namespace bankGoMyCode.Account
{
    [DataContract]
    [KnownType(typeof(Business<int , Transaction<int , int> , int , int >))]
    [KnownType(typeof(Saving<int, Transaction<int , int> , int , int >))]

    public abstract class AbstractAccount<TClientKey  , TTransaction , TAccountKey , TTransactionKey> : IAccount<TAccountKey, TTransaction , TTransactionKey> , IEquatable<AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey>>
        where TTransaction : Transaction<TTransactionKey , TAccountKey>  , new()
    {
        [DataMember]
        public double Balance { get; set; }
        [DataMember]
        public TClientKey Owner { get; set; }
        public DateTime Date { get; set; }
        [DataMember]
        public TAccountKey AccountNumber { get ; set ; }
        [DataMember]
        public State State { get ; set ; }
        [DataMember]
        public double TaxRatio { get; set; }
        [DataMember]
        public List<TTransaction> transactions { get; set; }

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

         public static bool operator ==(AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey> a, AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey> b)
        {
            return a.Equals(b);
            //AccountNumber.Equals(b.AccountNumber)
        }
        public static bool operator !=(AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey> a, AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey> b)
        {
            return !a.Equals(b);

            //return !a.AccountNumber.Equals(b.AccountNumber);
        }
            
        public bool Equals(AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey> other)
        {
            return AccountNumber.Equals(other.AccountNumber);
        }
            
        public virtual void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Outgoing, Transaction.State.Ready, transactionNumber, AccountNumber, targetKey, amount);
           // LoggingTransation((TTransaction)transaction);
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
            var log = new LoggerConfiguration()
           .WriteTo.File(@"C:\Users\achou\Desktop\bankGmc\Next-Lvl-D3otNet\bankGoMyCode\bank.log")
           .CreateLogger();
            log.Information("Transaction : Executing transaction " + transaction.State + " "  +  transaction.TransactionNumber); ;

        }

        public IEnumerable<TTransaction> GetTransactionsByDate(DateTime date)
        {
            return from t in transactions where t.Date.Equals(date) select t; 
        }

        public IEnumerable<TTransaction> GetTransactionsByTarget(TAccountKey targetAccountNumber)
        {
            return from t in transactions where t.Equals(targetAccountNumber) select t;
        }

        public IEnumerable<TTransaction> GetTransactionsByQuery(Func<TTransaction , bool> query)
        {
            return transactions.Where(query);
        }
    }
}
