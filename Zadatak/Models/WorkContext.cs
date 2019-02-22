using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Zadatak.Models
{
    public class WorkContext : DbContext
    {
        protected readonly IHostingEnvironment ho;

        public WorkContext(DbContextOptions<WorkContext> options, IHostingEnvironment ho)
            : base(options)
        {
            this.ho = ho;
            
        }


        public DbSet<Office> Offices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

    }
}
