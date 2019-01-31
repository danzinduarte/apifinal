using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    public class UsuarioController : Controller
    {
        private readonly IusuarioRepository _usuarioRepository;
        public UsuarioController(IusuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public ActionResult<RetornoView<Usuario>> GetAll()
        {
            return Ok (new{data = _usuarioRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public ActionResult<RetornoView<Usuario>> GetById(int id)
        {
            var usuario = _usuarioRepository.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(new {data = usuario});
        }

        [HttpPost]
        [Route("")]
        public ActionResult<RetornoView<Usuario>> Create([FromBody] Usuario usuario)
        {
            if(usuario.email == null)
            {
                return BadRequest();
            }
            
            try
            {
                if ( usuario.desativado == true)
                {
                    usuario.data_desativacao = DateTime.Now;
                }
               usuario.log_atualizacao = DateTime.Now;
               usuario.log_criacao = DateTime.Now;
               usuario.Validacoes();
               _usuarioRepository.Add(usuario);
            }
            catch (Exception)
            {
               var resultado = new RetornoView<Usuario>() { sucesso = false, mensagem = "falha ao criar o usuario"};
               return BadRequest(resultado);
            }
  
            var result = new RetornoView<Usuario>() { data = usuario, sucesso = true };
            return CreatedAtRoute("GetUsuario", new { id = usuario.id}, result);    
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoView<Usuario>> Update(int id, [FromBody] Usuario usuario)
        {

            var _usuario = _usuarioRepository.Find(id);
                    
            if(_usuario == null) 
            {
                return NotFound();
            }
            try 
            {
                if(usuario.desativado == true)
                {
                    usuario.data_desativacao = DateTime.Now;
                }

                _usuario.email              = usuario.email;
                _usuario.nome               = usuario.nome;
                _usuario.senha              = usuario.senha;
                _usuario.administrador      = usuario.administrador;
                _usuario.log_atualizacao    = DateTime.Now;
                _usuario.data_desativacao   = usuario.data_desativacao;

                _usuarioRepository.Update(_usuario);
            }
               
      
            catch (Exception)
            {
                var result = new RetornoView<Usuario>() { sucesso = false};
                return BadRequest(result);
            }

            var resultado = new RetornoView<Usuario>() { data = _usuario, sucesso = true, mensagem = "Usuario Atualizado com Sucesso!" };
            return resultado;
        }
       

        [HttpDelete("{id}")]
        public ActionResult<RetornoView<Usuario>> Delete(int id) 
        {
            var usuario  = _usuarioRepository.Find(id);
            if (usuario == null) 
            {
                return NotFound();
            }
            
            _usuarioRepository.Remove(id);
                            
            if (_usuarioRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<Usuario>() { sucesso = true };
                return resultado;
            }
            else 
            {
                var resultado = new RetornoView<Usuario>() { sucesso = false };
                return resultado;
            }
        }
    }
}
    