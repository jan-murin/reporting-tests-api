using System;
using System.Runtime.CompilerServices;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace Reporting.Tests.API.ExtentReport
{
    public class SpecflowExtentReportTestManager
    {
        [ThreadStatic]
        private static ExtentTest _gherkinFeature;

        [ThreadStatic]
        private static ExtentTest _gherkinScenario;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateGherkinFeature(string testName, string description = null)
        {
            _gherkinFeature = SpecflowExtentReportManager.Instance.CreateTest<Feature>(testName, description);
            return _gherkinFeature;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateGherkinScenario(string testName, string description = null)
        {
            _gherkinScenario = _gherkinFeature.CreateNode<Scenario>(testName, description);
            return _gherkinScenario;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetGherkinFeature()
        {
            return _gherkinFeature;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetGherkinScenario()
        {
            return _gherkinScenario;
        }
    }
}
