using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Zadatak.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class WorkContext : DbContext
    {
        /// <summary>
        /// The ho
        /// </summary>
        protected readonly IHostingEnvironment Ho;


        /// <summary>
        /// Initializes a new instance of the <see cref="WorkContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="ho">The ho.</param>
        public WorkContext(DbContextOptions<WorkContext> options, IHostingEnvironment ho)
            : base(options)
        {
            Ho = ho;
        }

        /// <summary>
        /// Gets or sets the offices.
        /// </summary>
        /// <value>
        /// The offices.
        /// </value>
        public DbSet<Office> Offices { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the device usages.
        /// </summary>
        /// <value>
        /// The device usages.
        /// </value>
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

    }
}
