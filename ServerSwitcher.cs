using System;
using System.IO;
using System.Net;

namespace ModLoaderAmongUs
{
    public class ServerSwitcher
    {
        public string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);


        public static void DatReplace(string filePath, string url, string title, string directory)
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

            Console.WriteLine($"Do you want to install the neccesary files for: '{title}' from ther internet? (y, yes/n, no)");


            bool datTransferSuccessful = false;
            //Location of download
            string datLocation = $@"{appFolder}\Mods\{directory}\regionInfo.dat";
            while (true)
            {
                string downloadModAns = Convert.ToString(Console.ReadLine());
                downloadModAns = downloadModAns.ToLower();

                if (downloadModAns == "y" || downloadModAns == "ye" || downloadModAns == "yes")
                {
                    Directory.CreateDirectory($@"{appFolder}\Mods\{directory}");
                    Console.WriteLine("The directory {0}\\Mods\\{1} was created successfully at {2}.\n", appFolder, directory, Directory.GetCreationTime($@"{appFolder}\Mods\Skeld_net"));
                    using var client = new WebClient();
                    client.DownloadFile(url, datLocation);
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
                    Console.WriteLine("Please type ('yes', 'y' / 'n', no')");

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

        private static void ReplaceDat(string fileToMoveAndDelete, string fileToReplace, bool metaDataSetting)
        {
            File.Delete(fileToReplace);
            File.Copy(fileToMoveAndDelete, fileToReplace, metaDataSetting);
        }


    }
}
