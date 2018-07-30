using Newtonsoft.Json;
using System;
using Teste_HubFintech.Data;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Business
{
    public class TransacoesBusiness
    {
        TransacoesData tData = new TransacoesData();
        //public void Incluir(Transacoes t)
        //{
        //    tData.Incluir(t);
        //}

        //public void Alterar(Transacoes t)
        //{
        //    tData.Alterar(t);
        //}

        //public void Excluir(int transacaoId)
        //{
        //    tData.Excluir(transacaoId);
        //}

        public string Consultar(int contaId)
        {
            return JsonConvert.SerializeObject(tData.Consultar(contaId));
        }

        public string MovimentacaoContas(Transacoes t)
        {
            if (t.TipoTransacao == (int)Transacoes.enuTipoTransacao.Estorno)
                return Estorno(t.TransacaoId, t.AporteId + "");
            else
            {
                string validacao = ValidarCampos(t);
                if (validacao.Length <= 0)
                {
                    if (t.TipoTransacao == (int)Transacoes.enuTipoTransacao.Aporte)
                    {
                        t.ContaIdOrigem = 0;
                        t.AporteId = tData.GerarNovoAporteId();
                    }

                    tData.Incluir(t);
                }
                return validacao;
            }
        }

        public string Estorno(int? transacaoId, string aporteId)
        {
            try
            {
                Transacoes t = new Transacoes();
                if (aporteId.Length > 0)
                    t = tData.ConsultarTransacao(null, aporteId);
                else if (transacaoId != null)
                    t = tData.ConsultarTransacao(transacaoId, "");

                if (t.TransacaoId <= 0)
                    return "Transação não localizada;";

                t.TipoTransacao = (int)Transacoes.enuTipoTransacao.Estorno;

                if (aporteId.Length > 0)
                    t.Valor = (t.Valor * -1);
                else if (transacaoId != null)
                {
                    int contaIdDestino = t.ContaIdOrigem.Value;
                    t.ContaIdOrigem = t.ContaIdDestino;
                    t.ContaIdDestino = contaIdDestino;
                }

                tData.Incluir(t);
                return "Estorno realizado com sucesso!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ValidarCampos(Transacoes t)
        {
            string msgRetorno = "";

            Transacoes.enuTipoTransacao tipo;
            if (!Enum.TryParse(t.TipoTransacao.ToString(), out tipo))
                msgRetorno += "Tipo de transação inexistente;";
            else
            {
                Contas contaDestino = RetornaCadastroConta(t.ContaIdDestino);
                Contas contaOrigem = RetornaCadastroConta(t.ContaIdOrigem.Value);
                Contas.enuTipoConta tipoContaDestino = ConsultaTipoConta(contaDestino);
                Contas.enuTipoConta tipoContaOrigem = ConsultaTipoConta(contaOrigem);

                if (contaDestino.ContaId <= 0)
                    msgRetorno += "Conta Destino inexistente;";

                if (!ValidaContaAtiva(contaDestino))
                    msgRetorno += "Conta Destino não está ativa;";

                if (tipo == Transacoes.enuTipoTransacao.Transferencia)
                {
                    if (tipoContaDestino == Contas.enuTipoConta.Matriz)
                        msgRetorno += "Contas Matriz não podem receber transferências;";

                    if (contaOrigem.ContaId <= 0)
                        msgRetorno += "Conta Origem inexistente;";
                    else
                    {
                        if (tipoContaOrigem == Contas.enuTipoConta.Matriz)
                            msgRetorno += "Contas Matriz não podem efetuar transferências;";
                        else
                        {
                            if (!ValidaContaAtiva(contaOrigem))
                                msgRetorno += "Conta Origem não está ativa;";
                        }
                    }

                    if(tipoContaDestino != Contas.enuTipoConta.Matriz && tipoContaOrigem != Contas.enuTipoConta.Matriz)
                    {
                        if (contaOrigem.ContaIdMatriz != contaDestino.ContaIdMatriz)
                            msgRetorno += "Conta Origem e Conta Destino não fazem parte da mesma árvore;";
                    }
                   
                }
            }

            if (t.Valor <= 0)
                msgRetorno += "Valor deve ser maior que zero;";

            return msgRetorno;
        }

        private Contas RetornaCadastroConta(int contaId)
        {
            ContasData pConta = new ContasData();
            return pConta.Consultar(contaId);
        }

        private Contas.enuTipoConta ConsultaTipoConta(Contas c)
        {
            Contas.enuTipoConta tipo = 0;

            if (c.ContaIdPai != null && c.ContaIdPai > 0)
                tipo = Contas.enuTipoConta.Filial;
            else if (c.ContaId > 0)
                tipo = Contas.enuTipoConta.Matriz;

            return tipo;
        }

        private bool ValidaContaAtiva(Contas c)
        {
            Contas.enuSituacao sit = (Contas.enuSituacao)c.Situacao;
            if (sit != Contas.enuSituacao.Ativa)
                return false;

            return true;
        }
    }
}
