using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.DeviceUsage}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IUsageRepository" />
    public class UsageRepository : Repository<DeviceUsage>, IUsageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UsageRepository(WorkContext context) : base(context)
        {
        }

    }
}
