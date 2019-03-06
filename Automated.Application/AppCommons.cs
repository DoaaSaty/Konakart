using Automated.Application;
using Automated.Utilities.AutomationAbstractions;
using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities;
using Automated.Utilities.Utilities.Parsers;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Automated.Application
{
    public  class AppCommons
    {
      
                
        public void Common_Setup()
        {
                      
            AppConfigs.Init();

            

        }//end method Common Setup

       
    }//end class TestCommons

}//end namespace Automated.Tests
