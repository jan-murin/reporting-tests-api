using System;
using Reporting.Tests.API.API.Extensions;
using TechTalk.SpecFlow;

namespace Reporting.Tests.API.Transformations
{
    [Binding]
    public class Transformations
    {
        [StepArgumentTransformation]
        public DateTime StringToDateTimeTransform(string day)
        {
            return DateTimeExtensions.StringToDateTimeNullableTransform(day) ?? DateTime.Now.Date;
        }

        [StepArgumentTransformation]
        public DateTime? StringToDateTimeNullableTransform(string day)
        {
            return DateTimeExtensions.StringToDateTimeNullableTransform(day);
        }
    }
}
