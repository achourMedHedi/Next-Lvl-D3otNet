using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using bankGoMyCode.Client;
using bankGoMyCode.Transaction;

namespace bankGoMyCode.Account
{
    [DataContract]

    public class Saving<TClientKey, TTransaction , TTransactionKey , TAccountKey > : AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey>
        where TTransaction : Transaction<TTransactionKey, TAccountKey> , new()
    {
        public Saving(TClientKey owner, TAccountKey accountNumber) : base(owner, accountNumber)
        {
            TaxRatio = 0.0001;
        }
        public override void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            double calculateResult = (TaxRatio * Balance) + amount;
            
            base.Debit(calculateResult,  transactionNumber,  targetKey); 
            
            //double calculateResult = ((Balance * (TaxRatio * amount))) + amount;
        }
    }
}
