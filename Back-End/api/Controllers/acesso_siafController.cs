using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    [Authorize()]
    public class acesso_siafController : Controller
    {
        private readonly Iacesso_siafRepository _acesso_siafRepository;
        public acesso_siafController(Iacesso_siafRepository acesso_siafRepository)
        {
            _acesso_siafRepository = acesso_siafRepository;
        }
        
        [HttpGet]
        public ActionResult<RetornoView<acesso_siaf>> GetAll()
        {
            return Ok (new{data = _acesso_siafRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "Getacesso_siaf")]
        public ActionResult<RetornoView<acesso_siaf>> GetById(int id)
        {
            var acesso_siaf = _acesso_siafRepository.Find(id);

            if (acesso_siaf == null)
            {
                return NotFound();
            }
            return Ok(new {data = acesso_siaf});
        }

        [HttpPost]
        [Route("")]
        public ActionResult<RetornoView<acesso_siaf>> Create([FromBody] acesso_siaf acesso_siaf)
        {
            try
           {
               acesso_siaf.datahoraacesso = DateTime.Now;
              if(acesso_siaf.androidgourmet == "S" )
              {
                  if(acesso_siaf.numdispositivo < 1)
                    {
                        return BadRequest();
                    }
              }
               if (acesso_siaf.androidgourmet == null)
               {
                   acesso_siaf.androidgourmet = "N";
                   acesso_siaf.numdispositivo = 0;
               }
               if(acesso_siaf.androidgourmet == "N")
               {
                   acesso_siaf.numdispositivo = 0;
               }
               if(acesso_siaf.androidpedidos == "S")
               {
                   if(acesso_siaf.numdispositivospedidos <1)
                   {
                       return BadRequest();
                   }
               }
               
               if(acesso_siaf.androidpedidos == null)
               {
                   acesso_siaf.androidpedidos = "N";
                   acesso_siaf.numdispositivospedidos = 0;
               }
                if(acesso_siaf.androidpedidos == "N")
                {
                    acesso_siaf.numdispositivospedidos = 0;
                }
                
               _acesso_siafRepository.Add(acesso_siaf);
           }
           catch (Exception e)
           {
               var resultado = new RetornoView<acesso_siaf>() { sucesso = false, erro = e.Message };
               return BadRequest(resultado);
           }
  
            var result = new RetornoView<acesso_siaf>() { data = acesso_siaf, sucesso = true };
            return CreatedAtRoute("Getacesso_siaf", new { id = acesso_siaf.id}, result);    
        }
        [HttpPut("{id}")]
        public ActionResult<RetornoView<acesso_siaf>> Update(int id, [FromBody] acesso_siaf acesso_siaf)
        {
            var _acesso_siaf = _acesso_siafRepository.Find(id);

            if(_acesso_siaf == null) 
            {
                return NotFound();
            }
            try
            {
                 acesso_siaf.datahoraacesso = DateTime.Now;
              
               if (acesso_siaf.androidgourmet == null)
               {
                   acesso_siaf.androidgourmet = "N";
                   acesso_siaf.numdispositivo = 0;
               }
               if(acesso_siaf.androidgourmet == "N")
               {
                   acesso_siaf.numdispositivo = 0;
               }
               
               if(acesso_siaf.androidpedidos == null)
               {
                   acesso_siaf.androidpedidos = "N";
                   acesso_siaf.numdispositivospedidos = 0;
               }
                if(acesso_siaf.androidpedidos == "N")
                {
                    acesso_siaf.numdispositivospedidos = 0;
                }

                _acesso_siafRepository.Update(_acesso_siaf);
            
             }
      
            catch (Exception)
            {
                var result = new RetornoView<Usuario>() { sucesso = false};
                return BadRequest(result);
            }

            var resultado = new RetornoView<acesso_siaf>() { data = _acesso_siaf, sucesso = true, erro = "Cliente Atualizado com Sucesso!" };
            return resultado;
     
        }

        [HttpDelete("{id}")]
        public ActionResult<RetornoView<acesso_siaf>> Delete(int id) 
        {
            var acesso_siaf  = _acesso_siafRepository.Find(id);
            if (acesso_siaf == null) 
            {
                return NotFound();
            }
            
            _acesso_siafRepository.Remove(id);
                            
            if (_acesso_siafRepository.Find(id) == null) 
            {
                var resultado = new RetornoView<acesso_siaf>() { sucesso = true };
                return resultado;
            }
            else 
            {
                var resultado = new RetornoView<acesso_siaf>() { sucesso = false };
                return resultado;
            }
        }
    }
}
    