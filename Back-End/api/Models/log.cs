using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("usuario_log")]
    public class usuario_log
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("usuario")]
        public int usuario_id { get; set; }
        public virtual Usuario usuario {get;set;}
        public DateTime data_hora {get;set;}
        public string acao {get;set;}
    }
}