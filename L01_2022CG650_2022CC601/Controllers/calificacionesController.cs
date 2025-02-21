using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022CG650_2022CC601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022CG650_2022CC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly BlogContext _blogContexto;

        public calificacionesController(BlogContext blogContext)
        {
            _blogContexto = blogContext;
        }

        //endpoin que retorna las faliciaciones
        [HttpGet]
        [Route("Todas las  calificaciones")]
        public IActionResult GetCalificaciones()
        {
            var listaCalificaciones = (from c in _blogContexto.calificaciones select c).ToList();
            if (listaCalificaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaCalificaciones);
        }

        
        [HttpGet]
        [Route("Calificaciones Por ID/{id}")]
        public IActionResult GetCalificacion(int id)
        {
            var calificacion = (from c in _blogContexto.calificaciones where c.calificacionId == id select c).FirstOrDefault();
            if (calificacion == null)
            {
                return NotFound();
            }
            return Ok(calificacion);
        }

        [HttpGet]
        [Route("Calificacion por puplicaciones/{publicacionId}")]
        public IActionResult Calificacionporpuplicaciones(int publicacionId)
        {
            var calificaciones = (from c in _blogContexto.calificaciones where c.publicacionId == publicacionId select c).ToList();
            if (calificaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(calificaciones);
        }

        [HttpPost]
        [Route("agregar Calificacion")]
        public IActionResult agregarCalificacion([FromBody] calificaciones calificacion)
        {
            try
            {
                _blogContexto.Add(calificacion);
                _blogContexto.SaveChanges();
                return Ok(calificacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar calificaiones /{id}")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] calificaciones calificacionModificar)
        {
            var calificacionActual = (from c in _blogContexto.calificaciones where c.calificacionId == id select c).FirstOrDefault();

            if (calificacionActual == null)
            {
                return NotFound();
            }

            calificacionActual.calificacion = calificacionModificar.calificacion;
            calificacionActual.publicacionId = calificacionModificar.publicacionId;
            calificacionActual.usuarioId = calificacionModificar.usuarioId;

            _blogContexto.Entry(calificacionActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(calificacionActual);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCalificacion(int id)
        {
            var calificacion = (from c in _blogContexto.calificaciones where c.calificacionId == id select c).FirstOrDefault();
            if (calificacion == null)
            {
                return NotFound();
            }

            _blogContexto.calificaciones.Remove(calificacion);
            _blogContexto.SaveChanges();

            return Ok(calificacion);
        }

        //endPoint que retorna los top N de usuarios y sus cantidades de comentarios registardas.


        [HttpGet]
        [Route("Obtener los usuarios de Top N esta en top 5")]
        public IActionResult GetTopUsuariosComentarios(int top = 5)
        {
            var topUsuarios = (from com in _blogContexto.comentarios group com by com.usuarioId into grupo
                                select new
                               {
                                   UsuarioId = grupo.Key,
                                   ComentariosCount = grupo.Count()
                               })
                               .OrderByDescending(x => x.ComentariosCount)
                               .Take(top)
                               .ToList();

            if (topUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(topUsuarios);
        }
    }
}
