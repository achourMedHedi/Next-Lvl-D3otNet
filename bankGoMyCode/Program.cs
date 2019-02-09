using System;
using System.Collections.Generic;
using bankGoMyCode.Account;
using bankGoMyCode.Client;
using bankGoMyCode.Transaction;

namespace bankGoMyCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Client<int,int,IAccount<int , Transaction<int , int> , int> , Transaction<int , int> > client = new Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>(1450, "sfsf");
            //Console.WriteLine(client.Name);
            Saving<int , Transaction<int, int> ,int , int > account = new Saving<int, Transaction<int, int>, int, int>(client.Cin , 123456);
            client.CreateAccount(account);
            //client.CloseAccount(account);
            foreach (var a in client.GetAllAccounts())
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
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
