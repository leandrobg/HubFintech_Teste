using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_HubFintech.Model
{
    public class Transacoes
    {
        public enum enuTipoTransacao
        {
            [Description("Aporte")]
            Aporte = 1,
            [Description("Transferência")]
            Transferencia = 2,
            [Description("Estorno")]
            Estorno = 3
        }

        public int TransacaoId { get; set; }
        public int? ContaIdOrigem { get; set; }
        public int ContaIdDestino { get; set; }
        public int TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Data { get; set; }
        public string AporteId { get; set; }
    }
}
