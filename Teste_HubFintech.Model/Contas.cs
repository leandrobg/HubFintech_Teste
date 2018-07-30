using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_HubFintech.Model
{
    public class Contas
    {
        public enum enuSituacao
        {
            [Description("Ativa")]
            Ativa = 1,
            [Description("Bloqueada")]
            Bloqueada = 2,
            [Description("Cancelada")]
            Cancelada = 3
        }

        public enum enuTipoConta
        {
            [Description("Matriz")]
            Matriz,
            [Description("Filial")]
            Filial
        }

        public int ContaId { get; set; }
        public string Nome { get; set; }
        public string DataCriacao { get; set; }
        public int? ContaIdPai { get; set; }
        public int Situacao { get; set; }
        public int PessoaId { get; set; }
        public int? ContaIdMatriz { get; set; }
    }
}
