using System.Globalization;
using Case2.Entities;
using System.IO;
using System.Collections.Generic;

namespace Case2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> employeesList = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fileds = sr.ReadLine().Split(',');
                        string name = fileds[0];
                        string email = fileds[1];
                        double salary = double.Parse(fileds[2], CultureInfo.InvariantCulture);
                        employeesList.Add(new Employee(name, email, salary));
                    }
                }
                Console.Write("Enter salary: ");
                double salaryAux = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emails = employeesList.Where(p => p.Salary > salaryAux).OrderBy(p => p.Email).Select(p => p.Email);
                foreach (string e in emails)
                {
                    Console.WriteLine(e);
                }

                var sum = employeesList.Where(obj => obj.Name[0] == 'M').Sum(obj => obj.Salary);

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2",CultureInfo.InvariantCulture));

            }
            catch (Exception e)
            {
                Console.WriteLine("Error !");
                Console.WriteLine(e.Message);
            }

        }
    }
}
