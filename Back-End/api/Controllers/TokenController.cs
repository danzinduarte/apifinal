using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataDbContext _context;
    
        public TokenController(IConfiguration configuration, DataDbContext ctx)
        {
            _configuration  = configuration;
            _context        = ctx;
        }

        [AllowAnonymous]
        [HttpPost]
         public IActionResult RequestToken([FromBody] Usuario request) {

             var usuario = _context.usuario
                     .Where(e => e.email == request.email)
                     .Where(e => e.senha == request.senha)
                     .FirstOrDefault();

                 if ((usuario != null) && (TrataHash.VerificaMD5Hash(request.senha, usuario.senha))) {
                     var claims = new[] {
                         new Claim(ClaimTypes.Name, request.email)
                     };
               
                
                    //Recebe uma instância da classe SymmetricSecurityKey
                    //Armazena a chave de criptografia usada na criação do token
                    var key = new SymmetricSecurityKey (
                                Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                    //Recebe um objeto do tipo SigninCredentials contendo a chave de
                    //criptografia e o algoritmo de segurança empregados na geração
                    //de assinaturas digitais para o tokens
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);          

                    var token = new JwtSecurityToken (
                        issuer              : "http://localhost:5000",
                        audience            : "http://localhost:5001/",
                        claims              : claims,
                        expires             : DateTime.Now.AddHours(1),
                        signingCredentials  : creds);

                        //Atualiza o Token do usuário
                        usuario.token = new JwtSecurityTokenHandler().WriteToken(token);
                            _context.usuario.Update(usuario);
                                _context.SaveChanges();

                        var resposta = new Object();

                    return Ok( new {
                        sucesso     = true,
                        mensagem    = "Bem vindo",
                        data        = resposta
                    });
                    
                }
                
            return Unauthorized("Credenciais Inválidas..");
        }
    }
}