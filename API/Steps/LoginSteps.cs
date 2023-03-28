using Reporting.Tests.API.API.Infrastructure;

namespace Reporting.Tests.API.API.Steps
{
    public class LoginSteps
    {
        public void LoginInWithNewCustomerAndDefaultEmployee(string customerName)
        {
            var customer = new CustomerInfo(customerName);
            Customers.RegisterCustomer(customer);
        }

        public void LoginInWithCustomerAndEmployee(string customerName, int employeeId, string employeeName)
        {
            var customer = Customers.GetCustomer(customerName);
            var employee = new EmployeeInfo
            {
                EmployeeId = employeeId,
                EmployeeFullName = employeeName,
                EmployeeInitials = employeeName.Substring(0, 2),
                EmployeeBirthNumber = "1111111111",
                EmployeeRoleId = 1,
                PositionPositionId = 1,
            };
            
            customer.RegisterEmployee(employee);
        }

        public void LoginInWithCustomerAndEmployeeWithFullAccess(string customerName, int employeeId, string employeeName)
        {
            var customer = Customers.GetCustomer(customerName);
            var employee = new EmployeeInfo
            {
                EmployeeId = employeeId,
                EmployeeFullName = employeeName,
                EmployeeInitials = employeeName.Substring(0, 2),
                EmployeeBirthNumber = "1111111111",
                EmployeeRoleId = 1,
                PositionPositionId = 1,
            };

            customer.RegisterEmployee(employee);
        }
    }
}
