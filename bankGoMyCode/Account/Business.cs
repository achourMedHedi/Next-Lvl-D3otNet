using bankGoMyCode.Transaction;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace bankGoMyCode.Account
{
    [DataContract]

    public class Business<TClientKey, TTransaction, TTransactionKey, TAccountKey> : AbstractAccount<TClientKey, TTransaction, TAccountKey, TTransactionKey>
        where TTransaction : Transaction<TTransactionKey, TAccountKey> , new()
    {
        public Business(TClientKey owner, TAccountKey accountNumber) : base(owner, accountNumber)
        {
            TaxRatio = 0.1;
        }
        public override void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            double calculateResult = amount + (amount*TaxRatio);

            base.Debit(calculateResult, transactionNumber, targetKey);

        }
    }
}
