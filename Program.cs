using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;


namespace ModLoaderAmongUs
{
    class Program
    {
        private const string PATH = "C:/Users/Tristan Isham/AppData/LocalLow/Innersloth/Among Us";

        static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to ModLoader - Among Us!");
            Greeting();
        }

        // Finds path to game for new users.
        //TODO: Add long terms retention so users don't need to do this every time
        public static void Greeting()
        {
            Console.WriteLine("First, what user are you? \n");
            int i = 0;
            Dictionary<int, string> pcUsers = new Dictionary<int, string>();
            try
            {
                //Goes through all directories in \Users and adds them to a dictionary.
                string[] dirs = Directory.GetDirectories(@"C:\Users\", "*");
                foreach (string dir in dirs)
                {

                    if (!pcUsers.ContainsKey(i))
                    {
                        pcUsers.Add(i, dir);
                    }

                    i++;
                }

                foreach (KeyValuePair<int, string> kv in pcUsers)
                {

                    Console.WriteLine("{0}", kv);
                }

                Console.WriteLine("\n");
                Console.WriteLine("Please enter number...");
                int userIndexNum = Convert.ToInt32(Console.ReadLine());
                //Validates user input 
                /* bool inputCheck = false;

                 foreach (var key in pcUsers.Keys)
                 {
                     if (key == userIndexNum)
                     {
                         inputCheck = true;

                     }
                     else
                     {
                         inputCheck = false;
                     }
                 }*/

                bool inputCheck = InputEval(pcUsers, userIndexNum);
                string gamePath = "";
                //Response to users
                while (true)
                {
                    if (inputCheck == true)
                    {
                        gamePath = $@"C:\Users\{pcUsers[userIndexNum]}\AppData\LocalLow\Innersloth\Among Us\";
                        break;
                    } //TODO: Need to improve error handling using while true and a method to capture user input. 
                    else
                    {
                        Console.WriteLine("Unintended Error: Please make sure you're not entering a key associated with:\n" +
                            "*Default User\n" +
                            "*Default\n" +
                            "OR\n" +
                            "*Public");
                        break;

                    }
                }

                //RESUME HERE TOMORROW
                DatReplace(gamePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static bool InputEval(Dictionary<int, string> dict, int input)
        {
            foreach (var key in dict.Keys)
            {
                if (key == input)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DatReplace(string filePath)
        {
            string _regionInfoDAT = $"{filePath}regionInfo.dat";



            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appData, "ModLoader_AmongUS");


            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                Console.WriteLine("The directory {0} was created successfully at {1}.", appFolder, Directory.GetCreationTime($"{appFolder}"));
            }


            if (!Directory.Exists($@"{appFolder}\Mods"))
            {
                Directory.CreateDirectory($@"{appFolder}\Mods");
                Console.WriteLine("The directory {0}\\Mods was created successfully at {1}.\n", appFolder, Directory.GetCreationTime($@"{appFolder}\Mods"));
            }

            Console.WriteLine("Do you want to install 'Skeld.net'? (y, yes/n, no");

            
            bool datTransferSuccessful = false;
            //Location of download
            string datLocation = $@"{appFolder}\Mods\Skeld_net\regionInfo.dat";
            while (true)
            {
                string downloadModAns = Convert.ToString(Console.ReadLine());
                downloadModAns = downloadModAns.ToLower();

                if (downloadModAns == "y" || downloadModAns == "ye" || downloadModAns == "yes")
                {
                    using var client = new WebClient();
                    client.DownloadFile("https://skeld.net/setup/regionInfo.dat", datLocation);
                    datTransferSuccessful = File.Exists(datLocation);
                    
                    break;
                } 
                else if (downloadModAns =="n" || downloadModAns =="no")
                {
                    Console.WriteLine("No other options available");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please type 'yes', 'y', or 'n', no'");

                }

            }

            if (datTransferSuccessful)
            {
                Console.WriteLine("regionInfo.dat created");
            }
            else if (!datTransferSuccessful)
            {
                Console.WriteLine("regionInfo.dat doesn't exist. Restart program and try again.");
                // TODO: Add logging
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Create error message. File wasn't created");
                //TODO: Add logging
                Environment.Exit(0);
            }

        }
    }
}
