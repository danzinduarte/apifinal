using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("usuario_permissao")]
    public class usuario_permissao
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("usuario")]
        public int usuario_id { get; set; }
        public virtual Usuario usuario {get;set;}
        public string rotina { get; set; }
    }
}