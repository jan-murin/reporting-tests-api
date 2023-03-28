using System;
using System.Runtime.CompilerServices;
using AventStack.ExtentReports;

namespace Reporting.Tests.API.ExtentReport
{
    public class ExtentReportTestManager
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest = ExtentReportManager.Instance.CreateTest(testName, description);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateChildTest(string testName, string description = null)
        {
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetParentTest()
        {
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetChildTest()
        {
            return _childTest;
        }

        public static void LogParentTestInfo(string message)
        {
            _parentTest?.Info(message);
        }

        public static void LogChildTestInfo(string message)
        {
            _childTest?.Info(message);
        }
    }
}
