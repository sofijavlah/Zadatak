using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.Interfaces;
using Zadatak.Models;
using Zadatak.Repositories;

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
        private IMapper _mapper;

        private IRepository<TEntity> _repository;

        private IUnitOfWork _unitOFWork;



        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TEntity, TDto}"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public BaseController(IMapper mapper, Repository<TEntity> repository, IUnitOfWork unitOFWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOFWork = unitOFWork;
        }

        // GET: api/Base
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            //var entities = _mapper.Map(_repository.GetAll().Select(x => new TDto()));
            return Ok(_mapper.Map(_repository.GetAll(), new List<TDto>()));
        }

        // GET: api/Base/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Get(long id)
        {
            var entity = _repository.Get(id);

            if (entity == null) return NotFound("Doesn't exist");

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
            _unitOFWork.Start();

            try
            {
                var entity = _mapper.Map(dto, new TEntity());

                _repository.Add(entity);
            }

            catch (Exception e)
            {
                throw new Exception("Cannot add");
            }

            _unitOFWork.Commit();

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

            if (entity == null) return BadRequest("Doesn't exist");

            Mapper.Map(dto, entity);

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

            if (entity == null) return BadRequest("Doesn't exist");
            try
            {
                _repository.Remove(entity);
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
