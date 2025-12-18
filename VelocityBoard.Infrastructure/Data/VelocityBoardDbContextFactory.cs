using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityBoard.Infrastructure.Data
{
    public class VelocityBoardDbContextFactory : IDesignTimeDbContextFactory<VelocityBoardDbContext>
    {
        public VelocityBoardDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../VelocityBoard.Web");
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VelocityBoardDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("VelocityBoardConnection"));

            return new VelocityBoardDbContext(optionsBuilder.Options);
        }
    }
}
