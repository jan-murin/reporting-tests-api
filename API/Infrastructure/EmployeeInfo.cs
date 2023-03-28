namespace Reporting.Tests.API.API.Infrastructure
{
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeInitials { get; set; }
        public string EmployeeBirthNumber { get; set; }
        public int EmployeeRoleId { get; set; }
        public int PositionPositionId { get; set; }

        internal EmployeeInfo DefaultEmployee(int orgId)
        {
            var employee = new EmployeeInfo
            {
                EmployeeId = orgId,
                EmployeeFullName = "DefaultEmployee",
                EmployeeInitials = "BN",
                EmployeeBirthNumber = "1234587",
                EmployeeRoleId = 3,
                PositionPositionId = 4,
            };

            return employee;
        }
    }
}
