using System;
using System.Collections.Generic;
using System.Text;
using bankGoMyCode.Client;
using bankGoMyCode.Transaction;

namespace bankGoMyCode.Account
{
    public class Saving<TClientKey, TTransaction , TTransactionKey , TAccountKey > : AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey>
        where TTransaction : Transaction<TTransactionKey, TAccountKey> , new()
    {
        public Saving(TClientKey owner, TAccountKey accountNumber) : base(owner, accountNumber)
        {
            TaxRatio = 0.0001;
        }
        public override void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            double calculateResult = (Balance - (Balance * (TaxRatio * amount))) - amount;
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Outgoing, Transaction.State.Ready, transactionNumber, AccountNumber, targetKey, amount);
            // try to log this action  
            LoggingTransation((TTransaction)transaction);
            // if (true) then accepte transaction and sendMoney
            if (Balance >= calculateResult)
            {
                transaction.State = Transaction.State.Accepted;
                DebitAccount(calculateResult, (TTransaction) transaction);
            }
            // else catch exception
            else
            {
                transaction.State = Transaction.State.Rejected;
                //DebitAccount(calculateResult, (TTransaction)transaction);
                LoggingTransation((TTransaction)transaction);
                throw new Exception("amount too high"); 
            }
        }

        public override void Credit(double amount , TAccountKey sourceTransactionKey , TTransactionKey transactionNumber)
        {
            if (State == State.Closed)
            {
                throw new Exception("account closed"); 
            }
            else
            {
                base.Credit(amount , sourceTransactionKey , transactionNumber);
            }
        }
    }
}
