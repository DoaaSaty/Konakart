using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Automated.Tests;

namespace Syngenta.GENIE.Automation.Test.Report
{
    public class ExtentManager
    {
        private static ExtentReports _extent;
        // private static ExtentXReporter _extentXReporter;
        private static KlovReporter _extentXReporter;
        private static ExtentTest _test;
        private static ExtentHtmlReporter _htmlReporter;
        private static string _filePath = TestConfigs.ReportUrl;
        private static string _reportName = TestConfigs.ReportName;

        public static ExtentReports GetExtent()
        {
            if (_extent != null)
                return _extent; // avoid creating new instance of html file
            _extent = new ExtentReports();
            _extent.AttachReporter(GetHtmlReporter());
            // extent.AttachReporter(GetHtmlReporter(), GetExtentXReporter());
            return _extent;
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            var path = Path.Combine(_filePath, _reportName);
            _htmlReporter = new ExtentHtmlReporter(path);

            // make the charts visible on report open
            _htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            _htmlReporter.Configuration().DocumentTitle = "Regression report";
            _htmlReporter.Configuration().ReportName = "Regression cycle";

            return _htmlReporter;
        }

        private static KlovReporter GetExtentXReporter()
        {
            //  _extentXReporter = new ExtentXReporter();
            _extentXReporter = new KlovReporter();
            _extentXReporter.Configuration().ReportName = "Build123";
            _extentXReporter.Configuration().ProjectName = "MyProject";
            _extentXReporter.Configuration().ServerURL = "http://localhost:1337/";
            return _extentXReporter;
        }
        //private static ExtentXReporter GetExtentXReporter()
        //{
        //    _extentXReporter = new ExtentXReporter();
        //    
        //    _extentXReporter.Configuration().ReportName = "Build123";
        //    _extentXReporter.Configuration().ProjectName = "MyProject";
        //    _extentXReporter.Configuration().ServerURL = "http://localhost:1337/";
        //    return _extentXReporter;
        //}


        public static ExtentTest CreateTest(string name, string description)
        {
            _test = _extent.CreateTest(name, description);
            return _test;
        }
    }
}

