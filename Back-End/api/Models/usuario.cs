using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validation;

namespace api.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public bool administrador { get; set; }
        public bool desativado { get; set; }
        public DateTime data_desativacao { get; set; }
        public DateTime log_criacao { get; set; }
        public DateTime log_atualizacao { get; set; }

         public void Validacoes()
        {
            AssertionConcern.AssertArgumentNotEquals(this.email, 0, "Email é obrigatório!");
            AssertionConcern.AssertArgumentNotEquals(this.nome, 0, "Nome é obrigatório!");
            AssertionConcern.AssertArgumentNotEquals(this.senha, 0, "Senha é obrigatória!");
        }
    }
}