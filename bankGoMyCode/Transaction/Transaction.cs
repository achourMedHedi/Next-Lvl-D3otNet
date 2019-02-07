using System;
using System.Collections.Generic;
using System.Text;


namespace bankGoMyCode.Transaction
{
    public class Transaction<TKey>
    {
        public TKey TransactionNumber { get; set; }
        public TKey SourceAccountNUmber { get; set; }
        public TKey TargetAccountNumber { get; set; }
        public double amount { get; set; }
        public DateTime date { get; set; }
        public State State { get; set; }
        public Direction Direction { get; set; }

    }
}
