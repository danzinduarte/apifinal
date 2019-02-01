using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validation;

namespace api.Models
{
    [Table("acesso_siaf")]
    public class acesso_siaf
    {
        [Key]
        public int id { get; set; }
        public string numeroserie { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; } 
        public string status { get; set; }
        public string contrato { get; set; }
        public DateTime datahoraacesso { get; set; }
        public string androidgourmet { get; set; }
        public int numdispositivo { get; set; }
        public string androidpedidos { get; set; }
        public int numdispositivospedidos { get; set; }
        public string observacao { get; set; }
        
    }
}