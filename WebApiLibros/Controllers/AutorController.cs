using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Entidades;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutorController(ApplicationDbContext context)
        {
            _context = context;
        }



        //TRAER TODOS
        [HttpGet]
        public List<Autor> Get()
        {
            //EF
            List<Autor> autores = _context.Autores.ToList();
            return autores;
        }



        //TRAER UNO
        [HttpGet("{id}")]
        public Autor Get(int id)
        {
            //EF
            Autor autor = _context.Autores.Find(id);

            return autor;
        }

        //INSERTAR 
        [HttpPost]
        public ActionResult Post(Autor autor)
        {
            //EF -- memoria
            _context.Autores.Add(autor);
            //EF - Guardar en la DB
            _context.SaveChanges();

            return Ok();
        }

        //MODIFICAR
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            //EF para modificar en la DB
            _context.Entry(autor).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //Eliminar

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            //EF
            var autor = _context.Autores.Find(id);

            if (autor == null)
            {
                return NotFound();
            }

            //EF
            _context.Autores.Remove(autor);
            _context.SaveChanges();

            return autor;

        }
    }
}
