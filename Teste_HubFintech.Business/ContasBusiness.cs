using Newtonsoft.Json;
using System;
using Teste_HubFintech.Data;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Business
{
    public class ContasBusiness
    {
        ContasData cData = new ContasData();
        public string Incluir(Contas c)
        {
            string validacao = ValidarCampos(c);
            if (validacao.Length <= 0)
                cData.Incluir(c);

            return validacao;
        }

        public string Alterar(Contas c)
        {
            string validacao = ValidarCampos(c);
            if (validacao.Length <= 0)
                cData.Alterar(c);

            return validacao;
        }

        public string Excluir(int contaId)
        {
            return cData.Excluir(contaId) + " registros afetados.";
        }

        public string Consultar(int contaId)
        {
            return JsonConvert.SerializeObject(cData.Consultar(contaId));
        }

        private string ValidarCampos(Contas c)
        {
            string msgRetorno = "";
            Contas.enuSituacao sit;
            if(!Enum.TryParse(c.Situacao.ToString(), out sit))
                msgRetorno += "Situação inválida;";

            if (c.PessoaId <= 0)
                msgRetorno += "PessoaId deve ser preenchido;";

            return msgRetorno;
        }
    }
}
