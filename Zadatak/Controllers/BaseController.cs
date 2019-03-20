using System;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.Exceptions;
using Zadatak.Interfaces;

namespace Zadatak.Controllers
{
    /// <summary>
    /// Base Controller
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
        private readonly IMapper _mapper;

        private readonly IRepository<TEntity> _repository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TEntity, TDto}"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        public BaseController(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [NoUnitOfWork]
        // GET: api/Base
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var entities = _repository.GetAll().Select(x => _mapper.Map<TDto>(x));
            return Ok(entities);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Doesn't exist</exception>
        [NoUnitOfWork]
        // GET: api/Base/5
        [HttpGet]
        public virtual IActionResult Get(long id)
        {
            var entity = _repository.Get(id);

            if (entity == null) throw new CustomException("Doesn't exist");

            var dto = _mapper.Map(entity, new TDto());

            return Ok(dto);
        }

        // POST: api/Base
        /// <summary>
        /// Posts the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Post(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                _repository.Add(entity);
            }

            catch (Exception)
            {
                throw new CustomException("Cannot add");
            }
            
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
        public virtual IActionResult Put(long id, TDto dto)
        {
            var entity = _repository.Get(id);
            
            _mapper.Map(dto, entity);
            
            return Ok("Changed");
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public virtual IActionResult Delete(long id)
        {
            var entity = _repository.Get(id);

            _repository.Remove(entity);
            
            return Ok("Deleted");
        }
    }
}
