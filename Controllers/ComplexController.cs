using Demo2.Context;
using Demo2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Demo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplexController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ComplexController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetComplex")]
        public ActionResult<IEnumerable<Complex>> GetAll()
        {
            return _context.Complexes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Complex> GetById(int id)
        {
            var complex = _context.Complexes.Find(id);

            if (complex == null) return NotFound("No se encontro el complejo loquitaaaa");

            return complex;
        }
    }
}
