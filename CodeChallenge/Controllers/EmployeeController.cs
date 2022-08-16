using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);
            
            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("Reports/{id}",Name = "getEmployeeReports")]
        public IActionResult GetEmployeeReports(string id)
        {
            _logger.LogDebug($"Received employee report get request for '{id}'");

            var employeeReportStructure = _employeeService.GetEmployeeReports(id);

            return Ok(employeeReportStructure);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }


        [HttpPost("Compensation")]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received employee create request for '{compensation.Employee.EmployeeId}");

            //_employeeService.Create(employee);

            return CreatedAtRoute("getCompensationById", new { id = compensation.CompensationId }, compensation);
        }

        [HttpGet("Compensation/{id}", Name = "GetCompensationById")]
        public IActionResult GetCompensationById(string id)
        {
            _logger.LogDebug($"Received employee report get request for '{id}'");

            var compensation = new Compensation { Employee = new Employee { EmployeeId = "b7839309-3348-463b-a7e3-5de1c168beb3" } };

            return Ok(compensation);
        }
    }
}
