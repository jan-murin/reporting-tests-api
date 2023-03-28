using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Reporting.Tests.API.API.Steps;
using Reporting.Tests.API.ExtentReport;
using TechTalk.SpecFlow;

namespace Reporting.Tests.API.Steps
{
    [Binding]
    public class Hooks : BaseSteps
    {
        public Hooks()
        {
        }
        
        [BeforeFeature(Order = 0)]
        public static void InitiateSpecflowFeatureForExtentReports()
        {
            ExtentReportTestManager.CreateParentTest(TestContext.CurrentContext.Test.Name);
        }

        [AfterFeature]
        public static void WriteSpecflowFeatureResultsToExtentReports()
        {
            if (ExtentReportTestManager.GetParentTest() == null)
            {
                return;
            }

            var extentTest = ExtentReportTestManager.GetParentTest();
            LogExtentTestReport(extentTest);

            ExtentReportManager.Instance.Flush();
        }

        [BeforeScenario(Order = 1)]
        public void InitiateTestScenarioForExtentReports()
        {
            if (ExtentReportTestManager.GetParentTest() == null)
            {
                return;
            }

            ExtentReportTestManager.CreateChildTest(TestContext.CurrentContext.Test.Name);
        }

        [BeforeScenario(Order = 2)]
        public void NewCustomer()
        {
            BeforeScenario();
            // _employeeContext.AddEmployee(Customers.CurrentCustomer.CurrentEmployee().EmployeeFullName, 
            //     Customers.CurrentCustomer.CurrentEmployee().EmployeeId);
        }

        [AfterScenario]
        public void WriteTestResultForExtentReports()
        {
            if (ExtentReportTestManager.GetChildTest() == null)
            {
                return;
            }

            var extentTest = ExtentReportTestManager.GetChildTest();
            LogExtentTestReport(extentTest);
        }

        private static void LogExtentTestReport(ExtentTest extentTest)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : $"<pre>{TestContext.CurrentContext.Result.StackTrace}</pre>";

            var message = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? ""
                : $"<pre>{TestContext.CurrentContext.Result.Message}</pre>";

            AventStack.ExtentReports.Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            extentTest.Log(logstatus, "Test ended with " + logstatus + message + stacktrace);
        }
    }
}
