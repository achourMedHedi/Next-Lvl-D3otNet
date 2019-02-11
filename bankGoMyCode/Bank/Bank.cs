using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using bankGoMyCode.Account;
using bankGoMyCode.Client;
using bankGoMyCode.CustomAttribute;
using bankGoMyCode.Transaction;
using Serilog;

namespace bankGoMyCode.Bank
{
    [DataContract]
    public class Bank<TBankKey, TClient , TTransaction, TTransactionKey, TAccountKey , TAccountEntity > : IBank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity>
        where TClient : IClient<TAccountEntity, TAccountKey>
        //where TAccountEntity : AbstractAccount<TAccountKey, TTransaction, TTransactionKey>
        where TAccountEntity : IAccount<TAccountKey, TTransaction, TTransactionKey>
        where TTransaction : Transaction<TTransactionKey, TAccountKey>
    {
        [DataMember]
        [Auther("achour")]
        public string Name { get; set; }
        [DataMember]
        [Auther("achour")]
        public TBankKey Swift { get; set; }
        [DataMember]
        public int Agent { get; set; }
        [DataMember]
        [Auther("achour everywhere")]
        public List<TClient> Clients { get; set; }
        public Queue<TTransaction> TransactionsQueue { get; set; }
        public Lazy<IEnumerable<TAccountEntity>> Accounts;
        public Lazy<IEnumerable<TTransaction>> Transactions;
        public readonly object lockTransaction = new object();
        Thread[] Agents;

        
        public Bank(string name , TBankKey swift )
        {
            Agents = new Thread[Agent];
            Name = name;
            Swift = swift;
            Agent = 1;
            Clients = new List<TClient>();
            TransactionsQueue = new Queue<TTransaction>();
            Accounts = new Lazy<IEnumerable<TAccountEntity>>(GetAllAccounts);
            Transactions = new Lazy<IEnumerable<TTransaction>>(GetAllTransactions);
            var log = new LoggerConfiguration()
             .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
             .CreateLogger();
            log.Information(name +"has been created ");
        }

        public IEnumerable<TTransaction> GetAllTransactions()
        {
            foreach (var client in Clients)
            {
                foreach (var account in client.GetAllAccounts())
                {
                    foreach (var transaction in account.GetAllTransactions())
                    {
                        yield return transaction;
                    }
                }
            }
        }

        public IEnumerable<TAccountEntity> GetAllAccounts()
        {
            foreach (var client in Clients)
            {
                foreach (var account in client.GetAllAccounts())
                {
                    yield return account;
                }
            }
        }

        public void AddTransaction(TTransactionKey number, TAccountKey source, TAccountKey target,double amount)
        {
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Incoming, bankGoMyCode.Transaction.State.Ready, number, source, target , amount);
            TransactionsQueue.Enqueue((TTransaction) transaction);
            Console.WriteLine("enqueue /*/*/*/*/*/*/**/*");
            TAccountEntity sender = Clients.Select(a => a.GetAccount(transaction.SourceAccountNUmber)).FirstOrDefault();
            TAccountEntity receiver = Clients.Select(a => a.GetAccount(transaction.TargetAccountNumber)).FirstOrDefault();

            for (int i = 0; i < Agent; i++)
            {
                Console.WriteLine("agent---------------------");
                Thread t = new Thread(new ThreadStart(() =>
                {
                    Console.WriteLine("thread-----------------");
                    lock (lockTransaction)
                    {
                        TransactionsQueue.Dequeue();
                        Console.WriteLine("dequeue /*/*/*/*/*/**/*");

                        Thread.Sleep(3000);
                        Console.WriteLine("lock--------------");
                        if (sender.Equals(receiver))
                        {
                            sender.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                        }
                        else 
                        {
                            try
                            {
                                sender.Debit(transaction.Amount, transaction.TransactionNumber, transaction.TargetAccountNumber);
                                receiver.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                            }
                            catch (Exception e)
                            {
                                
                                Console.WriteLine(e.Message);
                                
                            }
                        }
                        Console.WriteLine($"user receiver {receiver.Balance}");
                        Console.WriteLine($"//user sender {sender.Balance}");
                    }
                }));
                Console.WriteLine("start");
                 t.Start();
                Console.WriteLine("rawah");
            }
        }

        /*public void AddTransaction(TTransaction transaction)
        {
            // add transaction to the queue 
            // if theres an agent 
            //transactionsQueue.Enqueue(transaction);
            TransactionsQueue.Enqueue(transaction);
            Console.WriteLine("queue");
            if (Agent > 0)
            {
                Agent--;

                // lock 
                // sender account
                // receiver account 
                // sender.sendMoney
                // receiver.credit 
                TransactionsQueue.Dequeue();
                Console.WriteLine("dequeue");
                lock (TransactionsQueue)
                {
                   
                    // with thread :/
                    TAccountEntity sender = Clients.Select(a => a.GetAccount(transaction.SourceAccountNUmber)).FirstOrDefault();
                    TAccountEntity receiver = Clients.Select(a => a.GetAccount(transaction.TargetAccountNumber)).FirstOrDefault() ;
                    try
                    {
                        Console.WriteLine("d5alt");
                        Thread.Sleep(3000);
                        Console.WriteLine("lde5el");
                        // just to show you that i used operator overload in abstractAccount
                        if (sender == receiver && sender.State.Equals(Direction.Incoming))
                        {
                            receiver.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                        }
                        else if (sender == receiver && sender.State.Equals(Direction.Outgoing))
                        {
                            receiver.Credit(-transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                        }
                        else if (sender != receiver)
                        {
                            sender.Debit(transaction.Amount, transaction.TransactionNumber, transaction.TargetAccountNumber);
                            receiver.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }

            }
            else
            {
                Console.WriteLine("no agents ");
            }
            Agent++;
            
        }*/

        

        public void AddAgent()
        {
            Agent++;
            var log = new LoggerConfiguration()
          .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
          .CreateLogger();
            log.Information("Adding a agent"); 

        }

        public void AddAgent(int numberOfAgents)
        {
            Agent += numberOfAgents;
            var log = new LoggerConfiguration()
          .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
          .CreateLogger();
            log.Information("Adding "+numberOfAgents+" agent");
        }

       
        public void RemoveAgent()
        {
            Agent--;
            var log = new LoggerConfiguration()
          .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
          .CreateLogger();
            log.Information("remove 1 agent");
        }

        public void RemoveAgent(int numberOfAgents)
        {
            Agent -= numberOfAgents;
            var log = new LoggerConfiguration()
          .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
          .CreateLogger();
            log.Information("removing "+numberOfAgents+" agent");
        }

       

        public void Save(string filePath = @"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\Bank\Data.json")
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity>));
            ser.WriteObject(stream, this);

            stream.Position = 0;
            StreamReader seralize = new StreamReader(stream);
            File.WriteAllText(filePath, seralize.ReadToEnd());
            var log = new LoggerConfiguration()
         .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
         .CreateLogger();
            log.Information("bank saved");
        }

        public Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity> Load(string filePath = @"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\Bank\Data.json")
        {
            string path = File.ReadAllText(filePath);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(path));
            DataContractJsonSerializer serRead = new DataContractJsonSerializer(typeof(Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity>));

            stream.Position = 0;
            Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity> bank = (Bank<TBankKey, TClient, TTransaction, TTransactionKey, TAccountKey, TAccountEntity>)serRead.ReadObject(stream);
            return bank;
        }
    }
}
