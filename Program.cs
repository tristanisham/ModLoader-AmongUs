using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;


namespace ModLoaderAmongUs
{
    class Program
    {
        public const string SkeldNet = "https://skeld.net/setup/regionInfo.dat";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ModLoader - Among Us! (MULA)");
            Console.WriteLine("Copyright 2020 Tristan Isham");
            Greeting();
            Console.WriteLine("Thanks for using MULA!");
            Console.WriteLine("Press any key to close...");
            Console.ReadLine();
        }

        // Finds path to game for new users.
        //TODO: Add long terms retention so users don't need to do this every time
        public static void Greeting()
        {


            /*Console.WriteLine("First, what user are you? \n");
            int i = 0;
            Dictionary<int, string> pcUsers = new Dictionary<int, string>();*/
            try
            {


                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                //Creates File and Updates Game
                
                Console.WriteLine("Select Skeld.net?");
                Console.WriteLine("Please type ('yes', 'y' / 'n', no')");
                string downloadModAns = Console.ReadLine();
                downloadModAns = downloadModAns.ToString().Trim();

                while (true)
                {
                    
                    if (downloadModAns == "y" || downloadModAns == "ye" || downloadModAns == "yes")
                    {
                        DataClasses.Server skeld_net = new DataClasses.Server(SkeldNet, "Skeld.net", "Skeld_Net");

                        ServerSwitcher.DatReplace(appData, skeld_net.LocationOfDat, skeld_net.ModName, skeld_net.ModFolder);


                        break;
                    }
                    else if (downloadModAns == "n" || downloadModAns == "no")
                    {
                        Console.WriteLine("No other options available at this time");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Please type ('yes', 'y' / 'n', no'");

                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

/*        static void Ending()
        {
            Console.WriteLine("Press any key to close program...");
            Console.ReadLine();
        }*/


        // Add shit here if you want to kill classes.
/*        public struct Server
        {
            public string LocationOfDat;
            public string ModName;
            public string ModFolder;

            public Server(string url, string title, string directory)
            {
                LocationOfDat = url;
                ModName = title;
                ModFolder = directory;
            }


        }*/
    }
}
