using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Zadatak.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class WorkContext : DbContext
    {
        protected readonly IHostingEnvironment ho;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="ho">The ho.</param>
        //public WorkContext(DbContextOptions<WorkContext> options, IHostingEnvironment ho)
        //    : base(options)
        //{
        //    this.ho = ho;
        //}

        public DbSet<Office> Offices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

    }
}
