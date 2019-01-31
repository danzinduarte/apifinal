using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
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
    }
}