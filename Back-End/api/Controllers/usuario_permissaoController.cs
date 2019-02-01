using System.Collections.Generic;
using System.Linq;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    [Authorize()]
    public class usuario_permissaoController : Controller
    {
        private readonly Iusuario_permissaoRepository _usuario_permissaoRepository;
        public usuario_permissaoController(Iusuario_permissaoRepository usuario_permissaoRepository)
        {
            _usuario_permissaoRepository = usuario_permissaoRepository;
        }

        [HttpGet]
        public ActionResult<RetornoView<usuario_permissao>> GetAll()
        {
            return Ok (new{data = _usuario_permissaoRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "Getusuario_permissao")]
        public ActionResult<RetornoView<usuario_permissao>> GetById(int id)
        {
            var usuario_permissao = _usuario_permissaoRepository.Find(id);

            if (usuario_permissao == null)
            {
                return NotFound();
            }
            return Ok(new {data = usuario_permissao});
        }


        [HttpPost]
        [Route("")]
        public ActionResult<RetornoView<usuario_permissao>> Create([FromBody] usuario_permissao usuario_permissao)
        {
           
            try
            {

               _usuario_permissaoRepository.Add(usuario_permissao);
            }
            catch (Exception ex)
            {
               var resultado = new RetornoView<usuario_permissao>() { sucesso = false, erro = ex.Message };
               return BadRequest(resultado);
            }
  
            var result = new RetornoView<usuario_permissao>() { data = usuario_permissao, sucesso = true };
            return CreatedAtRoute("Getusuario_permissao", new { id = usuario_permissao.id}, result);    
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoView<usuario_permissao>> Update(int id, [FromBody] usuario_permissao usuario_permissao)
        {

            var _usuario_permissao = _usuario_permissaoRepository.Find(id);
                    
            if(_usuario_permissao == null) 
            {
                return NotFound();
            }
            try 
            {
                
                _usuario_permissaoRepository.Update(_usuario_permissao);
            }
               
      
            catch (Exception)
            {
                var result = new RetornoView<usuario_permissao>() { sucesso = false};
                return BadRequest(result);
            }

            var resultado = new RetornoView<usuario_permissao>() { data = _usuario_permissao, sucesso = true};
            return resultado;
        }
       

        [HttpDelete("{id}")]
        public ActionResult<RetornoView<usuario_permissao>> Delete(int id) 
        {
            var usuario  = _usuario_permissaoRepository.Find(id);
            if (usuario == null) 
            {
                return NotFound();
            }
            
            _usuario_permissaoRepository.Remove(id);
                            
            if (_usuario_permissaoRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<usuario_permissao>() { sucesso = true };
                return resultado;
            }
            else 
            {
                var resultado = new RetornoView<usuario_permissao>() { sucesso = false };
                return resultado;
            }
        }
    }
}
    