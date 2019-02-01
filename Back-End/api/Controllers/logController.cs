using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    [Authorize()]
    public class usuario_logController : Controller
    {
        private readonly Iusuario_logRepository _usuario_logRepository;
        public usuario_logController(Iusuario_logRepository usuario_logRepository)
        {
            _usuario_logRepository = usuario_logRepository;
        }

        [HttpGet]
        public ActionResult<RetornoView<usuario_log>> GetAll()
        {
            return Ok (new{data = _usuario_logRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "Getusuario_log")]
        public ActionResult<RetornoView<usuario_log>> GetById(int id)
        {
            var usuario_log = _usuario_logRepository.Find(id);

            if (usuario_log == null)
            {
                return NotFound();
            }
            return Ok(new {data = usuario_log});
        }
        [HttpPost]
        [Route("")]
        public ActionResult<RetornoView<usuario_log>> Create([FromBody] usuario_log usuario_log)
        {
           
            try
            {
               _usuario_logRepository.Add(usuario_log);
            }
            catch (Exception ex)
            {
               var resultado = new RetornoView<usuario_log>() { sucesso = false, erro = ex.Message };
               return BadRequest(resultado);
            }
  
            var result = new RetornoView<usuario_log>() { data = usuario_log, sucesso = true };
            return CreatedAtRoute("Getusuario_log", new { id = usuario_log.id}, result);    
        }
        
      
    }
}