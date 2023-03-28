using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Reporting.Tests.API.ExtentReport
{
    public class SpecflowExtentReportManager
    {
        private static readonly Lazy<ExtentReports> Lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance => Lazy.Value;

        static SpecflowExtentReportManager()
        {
            var htmlReporter = new ExtentHtmlReporter($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Reports\");

            Instance.AttachReporter(htmlReporter);
            Instance.AnalysisStrategy = AnalysisStrategy.BDD;
        }

        private SpecflowExtentReportManager()
        {
        }
    }
}
