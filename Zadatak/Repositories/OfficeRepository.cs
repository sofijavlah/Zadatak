using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// Office Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.Office}</cref>
    /// </seealso>
    /// <seealso cref="IOfficeRepository" />
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OfficeRepository(WorkContext context) : base(context)
        {
        }
    }
}
