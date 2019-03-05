using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<TEntity, TDto> : ControllerBase where TEntity : class, new() where TDto : class, new()
    {
        /// <summary>
        /// Gets or sets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected WorkContext Context { get; set; }

        /// <summary>
        /// Gets or sets the database set.
        /// </summary>
        /// <value>
        /// The database set.
        /// </value>
        protected DbSet<TEntity> DbSet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TEntity, TDto}"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public BaseController(IMapper mapper, WorkContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            Mapper = mapper;
        }

        // GET: api/Base
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        protected virtual IActionResult GetAll()
        {
            return Ok(DbSet.Select(x => Mapper.Map(x, new TDto())));
        }

        // GET: api/Base/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        protected virtual IActionResult Get(long id)
        {
            if (DbSet.Find(id) == null) return NotFound("Doesn't exist");
            return Ok(Mapper.Map(DbSet.Find(id), new TDto()));
        }

        // POST: api/Base
        /// <summary>
        /// Posts the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost("Post")]
        protected virtual IActionResult Post(TDto dto)
        {
            DbSet.Add(Mapper.Map(dto, new TEntity()));
            Context.SaveChanges();
            return Ok("Added");
        }

        // PUT: api/Base/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        protected virtual IActionResult Put(long id, TDto dto)
        {
            if (DbSet.Find(id) == null) return BadRequest("Doesn't exist");
            DbSet.Update(Mapper.Map(dto, DbSet.Find(id)));
            Context.SaveChanges();
            return Ok("Changed");
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        protected virtual IActionResult Delete(long id)
        {
            if (DbSet.Find(id) == null) return BadRequest("Doesn't exist");
            DbSet.Remove(DbSet.Find(id));
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException() is SqlException sqlException)
                {
                    var exNum = sqlException.Number;
                    if (exNum == 547) return BadRequest("Cannot be deleted");
                }
            }
            return Ok("Deleted");
        }
    }
}
