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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<TEntity, TDto> : ControllerBase where TEntity : class, new() where TDto : class, new()


    {
        private readonly IMapper _mapper;
        private readonly WorkContext context;
        private readonly DbSet<TEntity> dbSet;

        public BaseController(IMapper mapper, WorkContext contextt)
        {
            context = contextt;
            dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }

        // GET: api/Base
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            return Ok(dbSet.Select(x => _mapper.Map(x, new TDto())));
        }

        // GET: api/Base/5
        [HttpGet]
        public virtual IActionResult Get(long id)
        {
            if (dbSet.Find(id) == null) return NotFound();
            return Ok(_mapper.Map(dbSet.Find(id), new TDto()));
        }

        // POST: api/Base
        [HttpPost]
        public virtual IActionResult Post(TDto dto)
        {
            dbSet.Add(_mapper.Map(dto, new TEntity()));
            context.SaveChanges();
            return Ok("Added");
        }

        // PUT: api/Base/5
        [HttpPut]
        public virtual IActionResult Put(long id, TDto dto)
        {
            if (dbSet.Find(id) == null) return BadRequest("Doesn't exist");
            dbSet.Update(_mapper.Map(dto, dbSet.Find(id)));
            context.SaveChanges();
            return Ok("Changed");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public virtual IActionResult Delete(long id)
        {
            if (dbSet.Find(id) == null) return BadRequest("Doesn't exist");
            dbSet.Remove(dbSet.Find(id));
            try
            {
                context.SaveChanges();
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
