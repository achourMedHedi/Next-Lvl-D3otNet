using bankGoMyCode.Account;
using bankGoMyCode.Client;
using bankGoMyCode.CustomAttribute;
using bankGoMyCode.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace bankGoMyCode.Bank
{
    public static class BankExtensions
    {
        public static void AddClient(this Bank<int, Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>, Transaction<int, int>, int, int, IAccount<int, Transaction<int, int>, int>> bank, Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>> client)
        {
            var log = new LoggerConfiguration()
             .WriteTo.File(@"C:\Users\achou\source\repos\bankGoMyCode\bankGoMyCode\bank.log")
             .CreateLogger();
            log.Error("new client has beed created :" + client.Name);
            bank.Clients.Add(client);
           
        }
        public static Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>> GetClient(this Bank<int, Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>>, Transaction<int, int>, int, int, IAccount<int, Transaction<int, int>, int>> bank, int cin)
        {
            Client<int, int, IAccount<int, Transaction<int, int>, int>, Transaction<int, int>> client = (from c in bank.Clients where c.Equals(cin) select c).FirstOrDefault();
            if (client == null)
            {
                throw new Exception("account not found ");
            }
            return client;
        }


        public static void Auther(this object x)
        {
            //Console.WriteLine("methods");

            foreach (var propertyInfo in x.GetType().GetMethods())
            {
                //var propertyValue = propertyInfo.GetType();
                var autherAttribute = propertyInfo.GetCustomAttribute<AutherAttribute>();
                if (autherAttribute != null)
                {
                    Console.WriteLine(propertyInfo.Name + " auther = " + autherAttribute.name); ;
                }
            }
            //Console.WriteLine("attributes");

            foreach (var p in x.GetType().GetProperties())
            {
                //var propertyValue = propertyInfo.GetType();
                var autherAttribute = p.GetCustomAttribute<AutherAttribute>();
                if (autherAttribute != null)
                {
                    Console.WriteLine(p.Name + " auther = " + autherAttribute.name); ;
                }
            }
            Console.WriteLine("no auther");

        }
    }
}
