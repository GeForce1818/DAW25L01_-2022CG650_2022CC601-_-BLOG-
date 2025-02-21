using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2022CG650_2022CC601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2022CG650_2022CC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly BlogContext _blogContexto;

        public usuariosController(BlogContext blogContext)
        {
            _blogContexto = blogContext;
        }

        /// <summary>
        /// EndPoint que retorna el listado de todos los usuarios existentes
        /// </summary>
        ///
        [HttpGet]
        [Route("GetAllUsuarios")]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = (from u in _blogContexto.usuarios select u).ToList();
            if (listaUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaUsuarios);
        }

        [HttpGet]
        [Route("GetUsuariosPorNombreApellido")]
        public IActionResult GetUsuariosporNombreApellido(string nombre, string apellido)
        {
            var usuarios = (from u in _blogContexto.usuarios
                            where u.nombre.Contains(nombre) && u.apellido.Contains(apellido)
                            select u).ToList();
            if (usuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }


        [HttpGet]
        [Route("GetUsuariosPorRole/{rolId}")]
        public IActionResult GetUsuariosPorRole(int rolId)
        {
            var usuarios = (from u in _blogContexto.usuarios where u.rolId == rolId select u).ToList();
            if (usuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("AddUsuario")]
        public IActionResult AddUsuario([FromBody] usuarios usuario)
        {
            try
            {
                _blogContexto.Add(usuario);
                _blogContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] usuarios usuarioModificar)
        {
            var usuarioActual = (from u in _blogContexto.usuarios
                                 where u.usuarioId == id
                                 select u).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            // Actualizar campos
            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.clave = usuarioModificar.clave;
            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.apellido = usuarioModificar.apellido;
            usuarioActual.rolId = usuarioModificar.rolId;

            _blogContexto.Entry(usuarioActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(usuarioActual);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = (from u in _blogContexto.usuarios
                           where u.usuarioId == id
                           select u).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            _blogContexto.usuarios.Remove(usuario);
            _blogContexto.SaveChanges();

            return Ok(usuario);
        }
    }
}
