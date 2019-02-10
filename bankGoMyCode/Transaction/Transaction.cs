using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace bankGoMyCode.Transaction
{
    [DataContract]
    public class Transaction<TTransactionKey , TAccountkey> : IEquatable<TAccountkey>
    {
        [DataMember]
        public TTransactionKey TransactionNumber { get; set; }
        [DataMember]
        public TAccountkey SourceAccountNUmber { get; set; }
        [DataMember]
        public TAccountkey TargetAccountNumber { get; set; }
        [DataMember]
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        [DataMember]
        public State State { get; set; }
        [DataMember]
        public Direction Direction { get; set; }

        public Transaction() { }
        public Transaction(Direction direction , State state , TTransactionKey transactionNumber, TAccountkey sourceAccountNumber , TAccountkey targetAccountNumber , double amount )
        {
            TransactionNumber = transactionNumber;
            SourceAccountNUmber = sourceAccountNumber;
            TargetAccountNumber = targetAccountNumber;
            Amount = amount;
            Date = new DateTime();
            State = state;
            Direction = direction;
        }

        public bool Equals(TAccountkey other)
        {
            return TargetAccountNumber.Equals(other); 
        }
    }
}
