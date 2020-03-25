using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADAB
{
    

    public static class General
    {
        public static string AnyDeskConfigFolder { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AnyDesk\\";}
        public static string AnyDeskConfigUserFile { get => "user.conf"; }
    }
}
