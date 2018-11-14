using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace EmployeeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // filepath of CSV file
            string filePath = @"Employees.csv";

            // format output to a readable table layout
            const string format = "|{0,-10} |{1,-20} |{2,-26}";

            // variable used to increment EmployeeID for new employee
            int maxEmployeeID = 0;

            List<Employee> employees = new List<Employee>();

            // read from CSV file to list
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // loop through lines splitting columns by comma delimiter
            foreach (string line in lines)
            {
                string[] entries = line.Split(',');

                Employee employee = new Employee
                {
                    ID = entries[0],
                    Name = entries[1],
                    JobTitle = entries[2]
                };

                employees.Add(employee);
            }
            Console.WriteLine("Current Employee List:");

            //display contents of CSV file
            foreach (var employee in employees)
            {
                Console.WriteLine(string.Format(format, employee.ID, employee.Name, employee.JobTitle));
                if (!Regex.IsMatch(employee.ID, "[a-zA-Z]"))
                {
                    if (Int32.Parse(employee.ID) > maxEmployeeID)
                    {
                        maxEmployeeID = Int32.Parse(employee.ID);
                    }
                }
            }
            maxEmployeeID++;

            int selection = 0;
            while(selection != 3)
            {
                // menu
                Console.Write("\nMENU Options:"
                                  + "\n1) Add New Employee"
                                  + "\n2) Show Employee List"
                                  + "\n3) Exit"
                              + "\nSelection (1-3): ");

                bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out selection);

                if (parsedSuccessfully == false || selection < 1 || selection > 3)
                {
                    Console.WriteLine("Invalid selection! Please choose a valid option.\n");
                    continue;
                }

                // switch statement for menu options
                switch (selection)
                {
                    // add new employee
                    case 1:
                        Console.Write("Enter New Employee's Name (First Last): ");
                        string newEmpName = Console.ReadLine();

                        Console.Write("Enter New Employee's Job Title): ");
                        string newEmpJob = Console.ReadLine();

                        employees.Add(new Employee { ID = maxEmployeeID.ToString(), 
                            Name = newEmpName, JobTitle = newEmpJob });

                        List<string> output = new List<string>();

                        foreach (var employee in employees)
                        {
                            output.Add($"{employee.ID},{employee.Name},{employee.JobTitle}");
                        }

                        Console.WriteLine("Writing to Employee CSV file.");

                        File.WriteAllLines(filePath, output);

                        Console.WriteLine("All entries written.");

                        break;

                    // show current employee list
                    case 2:
                        foreach (var employee in employees)
                        {
                            Console.WriteLine(string.Format(format, employee.ID, employee.Name, employee.JobTitle));
                        }
                        break;

                    // exit program
                    case 3:
                        Console.WriteLine("Goodbye!");
                        break;
                }
            }
        }
    }
}
