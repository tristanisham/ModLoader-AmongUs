using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Net;

namespace ModLoaderAmongUs
{
    class Collections
    {

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

            /*string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);*/
            string appFolder = Path.Combine(filePath, "ModLoader_AmongUS");


            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                Console.WriteLine("The directory {0} was created successfully at {1}.", appFolder, Directory.GetCreationTime($"{appFolder}"));
            } //TODO: Add else that removes folder and rewrites everything for update functionality.


            if (!Directory.Exists($@"{appFolder}\Mods"))
            {
                Directory.CreateDirectory($@"{appFolder}\Mods");
                Console.WriteLine("The directory {0}\\Mods was created successfully at {1}.\n", appFolder, Directory.GetCreationTime($@"{appFolder}\Mods"));
            }

            Console.WriteLine("Do you want to install 'Skeld.net'? (y, yes/n, no)");


            bool datTransferSuccessful = false;
            //Location of download
            string datLocation = $@"{appFolder}\Mods\Skeld_net\regionInfo.dat";
            while (true)
            {
                string downloadModAns = Convert.ToString(Console.ReadLine());
                downloadModAns = downloadModAns.ToLower();

                if (downloadModAns == "y" || downloadModAns == "ye" || downloadModAns == "yes")
                {
                    Directory.CreateDirectory($@"{appFolder}\Mods\Skeld_net");
                    Console.WriteLine("The directory {0}\\Mods\\Skeld_net was created successfully at {1}.\n", appFolder, Directory.GetCreationTime($@"{appFolder}\Mods\Skeld_net"));
                    using var client = new WebClient();
                    client.DownloadFile("https://skeld.net/setup/regionInfo.dat", datLocation);
                    datTransferSuccessful = File.Exists(datLocation);

                    break;
                }
                else if (downloadModAns == "n" || downloadModAns == "no")
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

            //Add as constant later
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string amongUs = $@"{userPath}\AppData\LocalLow\Innersloth\Among Us";
            amongUs = Path.GetFullPath(amongUs);
            string datDestination = Path.GetFullPath($@"{amongUs}\regionInfo.dat");


            ReplaceDat(datLocation, datDestination, false);


            if (!File.Exists(amongUs + "\\regionInfo.dat"))
            {
                Console.WriteLine("Process failure");
            }
            else
            {
                Console.WriteLine("Process Success");
            }

            
        }

        public static void ReplaceDat(string fileToMoveAndDelete, string fileToReplace, bool metaDataSetting)
        {
            File.Delete(fileToReplace);
            File.Copy(fileToMoveAndDelete, fileToReplace, metaDataSetting);
        }
    }
}
