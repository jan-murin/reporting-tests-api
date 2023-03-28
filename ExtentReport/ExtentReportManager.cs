using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Reporting.Tests.API.ExtentReport
{
    public class ExtentReportManager
    {
        private static readonly Lazy<ExtentReports> Lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance => Lazy.Value;

        static ExtentReportManager()
        {
            var htmlReporter = new ExtentHtmlReporter($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Reports\");

            Instance.AttachReporter(htmlReporter);
        }

        private ExtentReportManager()
        {
        }
    }
}
