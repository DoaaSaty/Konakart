using Automated.Utilities.Utilities.Parsers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application
{
    public static class ApplicationConfigs
    {
        //Application Settings
        //public static string ObjectRepository { get; set; } = ConfigurationManager.AppSettings["ObjectRepository"];

        public static string FilePath { get; set; } = ConfigurationManager.AppSettings["FilePath"];


        //Application Settings
        public static string AutomationDirectory { get; set; }
        public static string ObjectRepository { get; set; }        

        public static void ReadConfigs(string pathOfCurrentContext = null)
        {
            if (pathOfCurrentContext != null)
            {
                //Application Settings
                AutomationDirectory = pathOfCurrentContext;

                //commented by asmaa on August 30th
                //ObjectRepository = pathOfCurrentContext + TextDataParser.GetValue("ObjectRepository");
            }
            else
            {
                //Application Settings
                AutomationDirectory = TextDataParser.GetValue("AutomationDirectory");
            }

            ObjectRepository = AutomationDirectory + TextDataParser.GetValue("ObjectRepository");


        }//end method ReadConfigs



    }//end class

}//end namespace
