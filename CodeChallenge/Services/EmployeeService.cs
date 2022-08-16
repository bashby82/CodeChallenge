using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IBaseRepository<Compensation> _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, 
                                IBaseRepository<Employee> employeeRepository, 
                                IBaseRepository<Compensation> compensationRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _compensationRepository = compensationRepository;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetEmployeeReports(string id)
        {
            var employee = GetById(id);
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = GetNumberOfReports(employee)
            };
        }

        private int GetNumberOfReports(Employee employee)
        {
            int totalReports = 0;

            if (employee.DirectReports == null)
                return totalReports;

            totalReports = employee.DirectReports.Count;

            foreach (var report in employee.DirectReports)
                totalReports += GetNumberOfReports(report);

            return totalReports;
        }

        public Compensation GetCompensationById(string id)
        {
            return string.IsNullOrEmpty(id) ? null : _compensationRepository.GetById(id);
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }
    }
}
