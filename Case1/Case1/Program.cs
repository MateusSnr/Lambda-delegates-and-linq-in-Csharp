using System.Collections.Generic;
using System.Globalization;
using Case1.Entities;
using System.Linq;
using System.IO;

namespace Case1{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Product> productsList = new List<Product>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    productsList.Add(new Product(name, price));
                }
            }
            var avg = productsList.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average price = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            var productsBelowAvg = productsList.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
            foreach (string p in productsBelowAvg)
            {
                Console.WriteLine(p);
            }
        }
    }
}
