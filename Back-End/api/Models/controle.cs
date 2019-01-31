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
        public string Razao_social { get; set; }
        public string Nome_fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Numero_serie { get; set; }
        public string Versao_siaf { get; set; }
        public string Ip { get; set; }
        public string Versao_ws { get; set; }
        public string Status_acesso { get; set; }
        public DateTime Data_hora_acesso { get; set; }
        public string Tipo_contrato { get; set; }
        public string Tipo_versao { get; set; }
    }
}