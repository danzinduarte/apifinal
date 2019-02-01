using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("controle_acesso")]
    public class controle_acesso
    {
        [Key]
        public int id { get; set; }
        public string razaosocial { get; set; }
        public string nomefantasia { get; set; }
        public string cnpj { get; set; }
        public string numeroserie { get; set; }
        public string versaosiaf { get; set; }
        public string ip { get; set; }
        public string versaows { get; set; }
        public string statusacesso { get; set; }
        public DateTime datahoraacesso { get; set; }
        public string tipocontrato { get; set; }
        public string tipoversao { get; set; }
    }
}