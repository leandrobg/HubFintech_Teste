using Newtonsoft.Json;
using System;
using Teste_HubFintech.Data;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Business
{
    public class PessoasBusiness
    {
        PessoasData pData = new PessoasData();
        public string Incluir(Pessoas p)
        {
            string validacao = ValidarCampos(p);
            if (validacao.Length <= 0)
            {
                DateTime data = Convert.ToDateTime(p.DataNascimento);
                p.DataNascimento = data.ToString("yyyy-MM-dd");
                pData.Incluir(p);
            }
            

            return validacao;
        }

        public string Alterar(Pessoas p)
        {
            string validacao = ValidarCampos(p);
            if (validacao.Length <= 0)
            {
                DateTime data = Convert.ToDateTime(p.DataNascimento);
                p.DataNascimento = data.ToString("yyyy-MM-dd");
                pData.Alterar(p);
            }

            return validacao;
        }

        public string Excluir(int pessoaId)
        {
            return pData.Excluir(pessoaId) + " registros afetados.";
        }

        public string Consultar(int pessoaId)
        {

            return JsonConvert.SerializeObject(pData.Consultar(pessoaId));
        }

        private string ValidarCampos(Pessoas p)
        {
            string msgRetorno = "";

            string cpfCnpjFormatado = p.CpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if ((cpfCnpjFormatado.Length != 11 && cpfCnpjFormatado.Length != 14))
            {
                msgRetorno += "CPF/CNPJ inválido;";
            }
            else
            {
                if (cpfCnpjFormatado.Length == 11)
                {
                    if (p.NomeCompleto.Length <= 0)
                        msgRetorno += "Nome Completo deve ser preenchido;";

                    if (p.DataNascimento.Length <= 0)
                        msgRetorno += "Data Nascimento deve ser preenchido;";
                }

                if (cpfCnpjFormatado.Length == 14)
                {
                    if (p.NomeFantasia.Length <= 0)
                        msgRetorno += "Nome Fantasia deve ser preenchido;";

                    if (p.RazaoSocial.Length <= 0)
                        msgRetorno += "Razão Social deve ser preenchido;";
                }
            }

            DateTime data = new DateTime();
            if (!DateTime.TryParse(p.DataNascimento, out data))
            {
                msgRetorno += "Data nascimento incorreta;";
            }

            return msgRetorno;
        }
    }
}
