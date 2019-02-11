using System;
using System.Collections.Generic;
using bankGoMyCode.Account;
using bankGoMyCode.Client;
using bankGoMyCode.Transaction;
using bankGoMyCode.Bank;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using System.Xml;
using Serilog;

namespace bankGoMyCode
{

    class Program
    {

        static void Main(string[] args)
        {
            Bank<int, Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>, Transaction<int, int>, int, int, IAccount<int, Transaction<int, int>, int>> bank = new Bank<int, Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>, Transaction<int, int>, int, int, IAccount<int, Transaction<int, int>, int>>("bmc bank ", 100);
            Client<int,int,IAccount<int , Transaction<int , int> , int> , Transaction<int , int> > client = new Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>(1450, "sfsf");
            bank.AddClient(client);
            Business<int , Transaction<int, int> ,int , int > account = new Business<int, Transaction<int, int>, int, int>(client.Cin ,123456);
            Business<int , Transaction<int, int> ,int , int > account2 = new Business<int, Transaction<int, int>, int, int>(client.Cin ,1234567);
            client.CreateAccount(account);
            client.CreateAccount(account2);
            bank.AddTransaction(10, 123456, 123456, 100);
            bank.AddTransaction(9, 123456, 1234567, 1000);
            //guid
            //Console.WriteLine(client.Name);
            //client.CloseAccount(account);
            /*foreach (var a in client.GetAllAccounts())
            {
                Console.WriteLine(a.AccountNumber.ToString() + " " + a.State);
            }

            Console.WriteLine(client.GetAccount(123456).AccountNumber);
            Console.WriteLine("credit" );
            account.Credit(500 ,11111, account.transactions.Count);
            Console.WriteLine("before " + account.Balance);

            account.Debit(100, account.transactions.Count, 125478);
            Console.WriteLine("after "+account.Balance);
            Console.WriteLine("------------------------");
            foreach (var tran in account.GetAllTransactions())
            {
                Console.WriteLine(tran.SourceAccountNUmber + " " + tran.TargetAccountNumber + " " + tran.State + " " + tran.Direction);
            }
            
            Console.WriteLine("-----------------------++++++++++++++++----------");
            //var allll = bank.accounts;
            foreach (var aaa in bank.Accounts.Value)
            {
                Console.WriteLine(aaa.AccountNumber);
            }
            bank.Save();

            Console.WriteLine(bank.Name);
            Console.WriteLine("-------------------");
            bank.Auther();*/
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
