using System.Collections.Generic;
using Automated.Utilities.Utilities.Parsers;

namespace Automated.Tests
{
    public static class TestData
    {
        /// <summary>
        /// This functions gets text that exceeds max characters and creates a new protocol
        /// </summary>
        /// <returns></returns>
        public static string GetTextExceedingMaximumCharacters()
        {
            //Get the current method name and current class name
            string methodName = "TextArea_MaximumCharacters";
            string sheetName = "Shared";

            //Get the data from the excel sheet
            string text = ExcelDataParser.GetValueOf("Description", methodName, sheetName);

            return text;

        }//end method
        
        public static string GetDummyText()
        {
            //Get the current method name and current class name
            string methodName = "DummyText";
            string sheetName = "Shared";

            //Get the data from the excel sheet
            string text = ExcelDataParser.GetValueOf("Description", methodName, sheetName);

            return text;

        }//end method

        public static string GetShortDummyText()
        {
            //Get the current method name and current class name
            string methodName = "ShortDummyText";
            string sheetName = "Shared";

            //Get the data from the excel sheet
            string text = ExcelDataParser.GetValueOf("Description", methodName, sheetName);

            return text;

        }//end method

    }//end class TestData


}//end namespace
