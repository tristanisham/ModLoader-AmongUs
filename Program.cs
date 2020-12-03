using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
                string gamePath ="";
                //Response to users
                while (true)
                {
                    if (inputCheck == true)
                    {
                        gamePath = $@"C:\Users\{pcUsers[userIndexNum]}\AppData\LocalLow\Innersloth\Among Us";
                        Console.WriteLine(gamePath);
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
    }
}
