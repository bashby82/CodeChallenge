using CodeChallenge.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Config
{
    public static class WebApplicationBuilderExt
    {
        private static readonly string DB_NAME = "EmployeeDB";
        private static readonly string COMP_DB_NAME = "CompensationDB";
        public static void UseEmployeeDB(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase(DB_NAME);
                options.UseInMemoryDatabase(COMP_DB_NAME);
            });
        }
    }
}
