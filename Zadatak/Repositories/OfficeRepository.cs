using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
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
    [DefineScopeType]
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OfficeRepository(WorkContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the office with given name
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        [NoUnitOfWork]
        public Office GetOffice(string description)
        {
            return Context.Offices.Include(x => x.Employees).FirstOrDefault(x => x.Description == description);
        }
    }
}
