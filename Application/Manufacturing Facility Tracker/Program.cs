using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing_Facility_Tracker
{
    public class Machine 
    {
        public string Name {get; set;}
        public string ID {get; set;}
        public int Temp {get; set;}
        public int Units {get; set;}
        public int Calls {get; set;}
        public override string ToString()
        {
            return "Name: " + Name + "ID: " + ID + "Temp: " + Temp + "Units: " + Units + "Calls: " + Calls;  //+ "Temp: " + Temp + "Units: " + Units
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string appName = "Machine Tracker";
            string appVersion = "1.0.0";
            string appAuthor = "Taylor Hasal";
            string machineCreate = "create [machineName] [machineID]\t\t-creates a new machine with specified name and ID";
            string machineAdd = "add [machineID] [numberOfUnits]\t\t\t-adds units to a specific machine";
            string machineTemp = "temperature [machineID] [temperature]\t\t-sets temperture of a specific machine";
            string setTemp = "temperature [machineID]\t\t\t\t-returns temperature of specific machine";
            string machineTotal = "total [machineID]\t\t\t\t-returns total units made my specific machine";
            string averageUnits = "average [machineID]\t\t\t\t-returns average units made per command by specific unit";
            string allMachines = "all\t\t\t\t\t\t-returns a list of the names and ID numbers of all machines";
            int numberOfMachines = 0;
            
            List<Machine> machines = new List<Machine>();

            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);
            Console.WriteLine("Enter 'help' to view a list of all acceptable inputs");

            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Please enter a command");
                
                string input = Console.ReadLine();
                string[] inputList = input.Split(' ');

                if (input == "help")
                {
                    Console.WriteLine("Acceptable inputs: \r\n{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}\r\n{6}", machineCreate, machineAdd, machineTemp, setTemp, machineTotal, averageUnits, allMachines);
                }
                //CREATE MACHINE
                else if (inputList[0].ToLower() == ("create"))
                {
                    if (inputList.Length == 3) 
                    {
                        int exists = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].Name == inputList[1] || machines[i].ID == inputList[2])
                            {
                                exists++;
                            }
                        }
                        if (exists == 0)
                        {
                            machines.Add(new Machine() {Name=inputList[1], ID=inputList[2], Temp=0, Units=0});
                            numberOfMachines++;
                            Console.WriteLine("{0} has been created with an ID:{1}", inputList[1], inputList[2]);
                        }
                        else 
                        {
                            Console.WriteLine("A machine with that name or ID already exists.");
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Please use correct format: create [machineName] [machineID]");
                    }
                }
                //ADD UNITS
                else if (inputList[0].ToLower() == ("add"))
                {
                    if (inputList.Length != 3)
                    {
                        Console.WriteLine("Please use correct format: add [machineID] [numberOfUnits]");
                    }
                    else if (!inputList[2].All(char.IsDigit))
                    {
                        Console.WriteLine("[numberOfUnits] must only contain numerical values");
                    }
                    else 
                    {
                        int found = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].ID == inputList[1])
                            {
                                machines[i].Units += int.Parse(inputList[2]);
                                machines[i].Calls++;
                                found++;
                                Console.WriteLine("{0} with ID:{1} has been assigned {2} units.", machines[i].Name, inputList[1], inputList[2]);
                            }
                        }
                        if (found == 0) 
                        {
                            Console.WriteLine("Machine with ID:{0} does not exist", inputList[1]);
                        }
                    }
                }
                //TEMPERATURE
                else if (inputList[0].ToLower() == ("temperature"))
                {
                    if (inputList.Length != 2 && inputList.Length != 3) 
                    {
                        Console.WriteLine("Please use correct format: temperature [machineID] [setTemperature] OR temperature [machineID]");
                    }
                    else if (inputList.Length == 2)
                    {
                        int found = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].ID == inputList[1])
                            {
                                found++;
                                Console.WriteLine("{0} with ID:{1} has a temperature of {2}.", machines[i].Name, inputList[1], machines[i].Temp);
                            }
                        }     
                        if (found == 0)
                        {
                            Console.WriteLine("Machine with ID:{0} does not exist", inputList[1]);
                        }
                    }
                    else if (inputList.Length == 3)
                    {
                        if (!inputList[2].All(char.IsDigit))
                        {
                            Console.WriteLine("[setTemperature] must only contain numerical values");
                        }
                        int found = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].ID == inputList[1])
                            {
                                found++;
                                machines[i].Temp = int.Parse(inputList[2]);
                                Console.WriteLine("The temperature of {0} with ID:{1} has been set to {2}.", machines[i].Name, inputList[1], machines[i].Temp);
                            }
                        }  
                        if (found == 0)
                        {
                            Console.WriteLine("Machine with ID:{0} does not exist", inputList[1]);      
                        }
                    }
                }
                //TOTAL UNITS
                else if (inputList[0].ToLower() == ("total"))
                {
                    if (inputList.Length != 2)
                    {
                        Console.WriteLine("Please use correct format: total [machineID]");
                    }
                    else
                    {
                        int found = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].ID == inputList[1])
                            {
                                found++;
                                Console.WriteLine("{0} with ID:{1} has created {2} total units.", machines[i].Name, inputList[1], machines[i].Units);
                            }
                        }
                        if (found == 0)
                        {
                            Console.WriteLine("Machine with ID:{0} does not exist", inputList[1]);
                        }
                    }
                }
                //AVERAGE UNITS
                else if (inputList[0].ToLower() == ("average"))
                {
                    if (inputList.Length != 2)
                    {
                        Console.WriteLine("Please use correct format: average [machineID]");
                    }
                    else
                    {
                        int found = 0;
                        decimal mean = 0;
                        for (int i = 0; i < numberOfMachines; i++)
                        {
                            if (machines[i].ID == inputList[1])
                            {
                                if (machines[i].Calls == 0)
                                {
                                    found++;
                                    Console.WriteLine("{0} with ID:{1} has not been called to produce any units.");
                                }
                                else
                                {
                                    mean = ((decimal)(machines[i].Units)/100) / ((decimal)(machines[i].Calls)/100);
                                    found++;
                                    Console.WriteLine("{0} with ID:{1} has created an average of {2} units per call.", machines[i].Name, inputList[1], mean);
                                }
                            }
                        }
                        if (found == 0)
                        {
                            Console.WriteLine("Machine with ID:{0} does not exist", inputList[1]);
                        }
                    }

                }
                else if (input == "all")
                {
                    for (int i = 0; i < numberOfMachines; i++)
                    {
                        Console.WriteLine("{0}. Name: {1}, ID: {2}, Units: {3}, Calls: {4}, Temperature: {5}", i+1, machines[i].Name, machines[i].ID, machines[i].Units, machines[i].Calls, machines[i].Temp);
                    }                          
                }
                else
                {
                    Console.WriteLine("{0} is not a valid input. Please type 'help' to see a list of acceptable inputs", inputList[0]);
                }
            }
        }
    }
}
