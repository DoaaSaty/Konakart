using Automated.Utilities.Utilities;
using Automated.Utilities.Utilities.Parsers;
using System.Configuration;
using System.Linq;

namespace Automated.Application
{
   public  class AppConfigs
    {
        // Initialize long and short time
        public static int longTime;
        public static int medTime;
        public static int shortTime;
        public static string filePath;

        public static bool IsTestConfigsInitialized = false;

        /// <summary>
        /// Read the provided test configurations
        /// </summary>
        public static void ReadConfigs()
        {
            // Waiting time: long and short

            shortTime = int.Parse(ConfigurationManager.AppSettings["ShortTime"]);
           // medTime = int.Parse(ConfigurationManager.AppSettings["MediumTime"]);
            longTime = int.Parse(ConfigurationManager.AppSettings["LongTime"]);

            //ImportFiles path
            filePath = ConfigurationManager.AppSettings["FilePath"];

        }//end method ReadConfigs

        /// <summary>
        /// Initialize the test configurations
        /// </summary>
        public static void Init()
        {
            if (!IsTestConfigsInitialized)
            {
                //Read the automated app configs
                ReadConfigs();
                IsTestConfigsInitialized = true;

            }//endif

        }//end method
        

    }
}
