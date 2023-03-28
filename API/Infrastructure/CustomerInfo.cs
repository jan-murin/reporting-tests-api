using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Reporting.Tests.API.API.Infrastructure
{
    public class CustomerInfo
    {
        public string CustomerName { get; set; }
        public Guid Guid { get; set; }
        public int OrgId { get; set; }

        private readonly List<EmployeeInfo> _employees = new List<EmployeeInfo>();
        private EmployeeInfo _currentEmployee;

        public CustomerInfo(string customerName = "CustomerName")
        {
            CustomerName = $"{customerName}_{TestContext.CurrentContext.Test.Name}";
            Guid = Guid.NewGuid();
            OrgId = new Random((int)DateTime.Now.Ticks).Next(1, Int32.MaxValue);
            var employee = new EmployeeInfo().DefaultEmployee(OrgId);
            RegisterEmployee(employee);
            SetCurrentEmployee(employee.EmployeeId);
        }

        public EmployeeInfo CurrentEmployee()
        {
            return _currentEmployee;
        }

        public void RegisterEmployee(EmployeeInfo employee)
        {
            var existingEmployee = _employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                SetCurrentEmployee(existingEmployee.EmployeeId);
            }
            else
            {
                _employees.Add(employee);
                SetCurrentEmployee(employee.EmployeeId);
            }
        }

        public void SetCurrentEmployee(int employeeId)
        {
            _currentEmployee = _employees.First(x => x.EmployeeId == employeeId);
        }

        public void UpdateCurrentEmployee(EmployeeInfo employee)
        {
            var existingEmployee = _employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
            if (existingEmployee == null)
            {
                throw new Exception($"Unable to find employee by id: {employee.EmployeeId}.");
            }

            _employees.Remove(employee);
            _employees.Add(employee);
            SetCurrentEmployee(employee.EmployeeId);
        }

        public EmployeeInfo GetEmployeeByName(string employeeName)
        {
            var employee = _employees.First(x => x.EmployeeFullName.Contains(employeeName));
            if (employee == null)
            {
                throw new Exception($"Employee by name: {employeeName} not found.");
            }
            return employee;
        }
    }
}
