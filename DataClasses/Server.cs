using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoaderAmongUs.DataClasses
{
    class Server
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
    }
}
