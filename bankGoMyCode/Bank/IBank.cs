using System;
using System.Collections.Generic;
using System.Text;
using bankGoMyCode.Account;
using bankGoMyCode.Bank;
using bankGoMyCode.Client;
using bankGoMyCode.Transaction;

namespace bankGoMyCode.Bank
{
    public interface IBank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity>
        where TClient : IClient<TAccountEntity, TAccountKey> 
        where TAccountEntity : IAccount<TAccountKey, TTransaction , TTransactionKey>
        where TTransaction : Transaction<TTransactionKey , TAccountKey>
    {
        List<TClient> Clients { get; set; }

        void AddAgent();
        void AddAgent(int numberOfAgents);
        void RemoveAgent();
        void RemoveAgent(int numberOfAgents);
        Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity> Load(string filePath);

        void Save(string filePath);
        void AddTransaction(TTransaction transaction);
    }
}
