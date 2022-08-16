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
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<IBaseRepository<Compensation>> _logger;

        public CompensationRepository(ILogger<IBaseRepository<Compensation>> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationContext.Compensation.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var list = _compensationContext.Compensation.ToList();
            return list.SingleOrDefault(c => c.CompensationId == id);
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
