using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.Account
{
    public class AbstractAccount<TClientEntity  , TTransaction>
    {
        public double Balance { get; set; }
        public Guid AccountNumber { get; set; }
        public TClientEntity Owner { get; set; }
        public List<TTransaction> transactions { get; set; }
        public DateTime Date { get; set; }
        public State State { get; set; }

        public AbstractAccount(TClientEntity owner)
        {
            Balance = 0;
            AccountNumber = Guid.NewGuid();
            Owner = owner;
            transactions = new List<TTransaction>();
            Date = new DateTime();
            State = State.Active;

        }
        
    }
}
