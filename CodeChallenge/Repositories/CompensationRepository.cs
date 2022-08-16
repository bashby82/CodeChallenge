using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : IBaseRepository<Compensation>
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IBaseRepository<Compensation>> _logger;

        public CompensationRepository(ILogger<IBaseRepository<Compensation>> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensation.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var list = _employeeContext.Compensation.ToList();
            return list.SingleOrDefault(c => c.CompensationId == id);
        }

        public Compensation Remove(Compensation compensation)
        {
            return _employeeContext.Remove(compensation).Entity;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
