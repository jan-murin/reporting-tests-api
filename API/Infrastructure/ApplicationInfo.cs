using System;

namespace Reporting.Tests.API.API.Infrastructure
{
    public static class ApplicationInfo
    {
        [ThreadStatic] public static string ApplicationId;

        private static readonly Guid PPT = Guid.Parse("d0bcc12e-008a-4027-999d-d0629a8c1dc5");
        private static readonly Guid BVV = Guid.Parse("617991e2-a0ab-4027-93ed-b2f402af4163");
        private static readonly Guid BV =  Guid.Parse("d5269093-806f-4d48-bc8d-d3e6d58fd250");
        private static readonly Guid SCC = Guid.Parse("7f2c9fea-4fd3-4482-be7b-1aaff5902664");
        private static readonly Guid CAR = Guid.Parse("b54a9261-3d11-4f92-8ce0-dbdb4a34868d");

        public static void RecreateApplicationId()
        {
            ApplicationId = Guid.NewGuid().ToString();
        }

        public static string GetString(this ApplicationType applicationType)
        {
            return Enum.GetName(applicationType.GetType(), applicationType);
        }

        public static Guid GetApplicationId(this ApplicationType type)
        {
            switch (type)
            {
                case ApplicationType.BVV:
                    return BVV;
                case ApplicationType.BV:
                    return BV;
                case ApplicationType.PPT:
                    return PPT;
                case ApplicationType.CAR:
                    return CAR;
                case ApplicationType.SCC:
                    return SCC;
            }

            throw new Exception("Unknown application type");
        }
    }

    public enum ApplicationType
    {
        PPT = 0,
        BVV = 1,
        SCC = 2,
        CAR = 3,
        BV = 4,

        FollowUp = 100,
        Appointment = 200,
        Diagnosis = 300,
        Assessment = 400,
        ReportAndInvestigation = 500,
        ProfilApi = 600,
        ActivityLog = 700,
        PlanAndEvaluation = 800,
        PatientDocumentation = 900,
        Decisions = 1000,
        Measures = 1100
    }
}