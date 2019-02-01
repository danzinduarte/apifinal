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
        public string token { get; set; }

        public void Validacoes()
        {
            AssertionConcern.AssertArgumentLength(this.nome,3 , 40, "O nome deve conter no minimo 3 caracteres e no maximo 40 !");
            //AssertionConcern.AssertArgumentLength(this.senha,6, 24, "A senha deve conter no minimo 6 caracteres e no maximo 24!");
        }
    }
}