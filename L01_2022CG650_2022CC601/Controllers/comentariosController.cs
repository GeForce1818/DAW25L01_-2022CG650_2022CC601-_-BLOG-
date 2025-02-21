using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022CG650_2022CC601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022CG650_2022CC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {

        private readonly BlogContext _blogContexto;

        public comentariosController(BlogContext blogContext)
        {
            _blogContexto = blogContext;
        }

        
        [HttpGet]
        [Route("GetAllComentarios/Ver todos los comentarios :D")]
        public IActionResult GetComentarios()
        {
            var listaComentarios = (from c in _blogContexto.comentarios select c).ToList();
            if (listaComentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaComentarios);
        }

        
        [HttpGet]
        [Route("GetByIdComentario/Filtro por Id/{id}")]
        public IActionResult GetComentario(int id)
        {
            var comentario = (from c in _blogContexto.comentarios where c.cometarioId == id select c).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        
        [HttpGet]
        [Route("GetComentariosByUsuario/Filtro por usuario/{usuarioId}")]
        public IActionResult GetComentariosByUsuario(int usuarioId)
        {
            var comentarios = (from c in _blogContexto.comentarios where c.usuarioId == usuarioId select c).ToList();
            if (comentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(comentarios);
        }

        
        [HttpPost]
        [Route("AddComentario/Añadir comentarios")]
        public IActionResult AddComentario([FromBody] comentarios comentario)
        {
            try
            {
                _blogContexto.Add(comentario);
                _blogContexto.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut]
        [Route("Actualizar comentarios/{id}")]
        public IActionResult ActualizarComentario(int id, [FromBody] comentarios comentarioModificar)
        {
            var comentarioActual = (from c in _blogContexto.comentarios where c.cometarioId == id select c).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.comentario = comentarioModificar.comentario;
            comentarioActual.publicacionId = comentarioModificar.publicacionId;
            comentarioActual.usuarioId = comentarioModificar.usuarioId;

            _blogContexto.Entry(comentarioActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(comentarioActual);
        }

        
        [HttpDelete]
        [Route("Eliminar comentarios/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            var comentario = (from c in _blogContexto.comentarios where c.cometarioId == id select c).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }

            _blogContexto.comentarios.Remove(comentario);
            _blogContexto.SaveChanges();

            return Ok(comentario);
        }


    }
}
