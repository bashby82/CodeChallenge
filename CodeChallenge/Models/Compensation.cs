using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public Employee employee { get; set; }
        public decimal salary { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
