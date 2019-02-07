using System;
using System.Collections.Generic;
using System.Text;


namespace bankGoMyCode.Transaction
{
    public class Transaction<TTransactionKey , TAccountkey>
    {
        public TTransactionKey TransactionNumber { get; set; }
        public TAccountkey SourceAccountNUmber { get; set; }
        public TAccountkey TargetAccountNumber { get; set; }
        public double amount { get; set; }
        public DateTime date { get; set; }
        public State State { get; set; }
        public Direction Direction { get; set; }

    }
}
