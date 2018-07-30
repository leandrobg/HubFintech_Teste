using System;
using System.Text;
using Teste_HubFintech.Model;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Teste_HubFintech.Data
{
    public class TransacoesData
    {
        ConnectionFactory objCon = new ConnectionFactory();

        public void Incluir(Transacoes t)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO tbTransacoes (ContaIdOrigem, ContaIdDestino, TipoTransacao, Valor, Data, AporteId) ")
                     .AppendLine("VALUES(@ContaIdOrigem, @ContaIdDestino, @TipoTransacao, @Valor, DATE('now'), @AporteId) ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@ContaIdOrigem", (t.TipoTransacao == (int)Transacoes.enuTipoTransacao.Aporte ? null : t.ContaIdOrigem));
                    cmd.Parameters.AddWithValue("@ContaIdDestino", t.ContaIdDestino);
                    cmd.Parameters.AddWithValue("@TipoTransacao", t.TipoTransacao);
                    cmd.Parameters.AddWithValue("@Valor", t.Valor);
                    cmd.Parameters.AddWithValue("@AporteId", t.AporteId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCon.conn.State == System.Data.ConnectionState.Open)
                    objCon.conn.Close();
            }
        }
        public void Alterar(Transacoes t)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE tbTransacoes ")
                     .AppendLine("SET ContaIdOrigem = @ContaIdOrigem ")
                     .AppendLine("   ,ContaIdDestino = @ContaIdDestino ")
                     .AppendLine("   ,TipoTransacao = @TipoTransacao ")
                     .AppendLine("   ,Valor = @Valor ")
                     .AppendLine("   ,Data = @Data ")
                     .AppendLine("WHERE TransacaoId = @TransacaoId ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@TransacaoId", t.TransacaoId);
                    cmd.Parameters.AddWithValue("@ContaIdOrigem", t.ContaIdOrigem);
                    cmd.Parameters.AddWithValue("@ContaIdDestino", t.ContaIdDestino);
                    cmd.Parameters.AddWithValue("@TipoTransacao", t.TipoTransacao);
                    cmd.Parameters.AddWithValue("@Valor", t.Valor);
                    cmd.Parameters.AddWithValue("@Data", t.Data);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCon.conn.State == System.Data.ConnectionState.Open)
                    objCon.conn.Close();
            }
        }
        public void Excluir(int transacaoId)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM tbTransacoes WHERE TransacaoId = @TransacaoId ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@TransacaoId", transacaoId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCon.conn.State == System.Data.ConnectionState.Open)
                    objCon.conn.Close();
            }
        }
        public List<Transacoes> Consultar(int contaId)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT TransacaoId, ContaIdOrigem, ContaIdDestino, TipoTransacao, Valor, Data, AporteId")
                     .AppendLine("FROM tbTransacoes ");
                query.AppendLine("WHERE (ContaIdOrigem = @ContaId ")
                     .AppendLine("OR     ContaIdDestino = @ContaId) ");

                List<Transacoes> retorno = new List<Transacoes>();

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@ContaId", contaId);
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            retorno.Add(
                                new Transacoes
                                {
                                    TransacaoId = Convert.ToInt32(dr["TransacaoId"]),
                                    ContaIdOrigem = Convert.ToInt32(dr["ContaIdOrigem"].ToString()),
                                    ContaIdDestino = Convert.ToInt32(dr["ContaIdDestino"].ToString()),
                                    TipoTransacao = Convert.ToInt32(dr["TipoTransacao"]),
                                    Valor = Convert.ToDecimal(dr["Valor"]),
                                    Data = dr["Data"].ToString(),
                                    AporteId = dr["AporteId"].ToString()
                                }
                            );
                        }
                    }
                }

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objCon.conn.Close();
            }
        }
        public Transacoes ConsultarTransacao(int? transacaoId, string aporteId = "")
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT TransacaoId, ContaIdOrigem, ContaIdDestino, TipoTransacao, Valor, Data, AporteId")
                     .AppendLine("FROM tbTransacoes ");

                if (transacaoId != null)
                {
                    query.AppendLine("WHERE TransacaoId = @TransacaoId ");
                }
                else if (aporteId.Length > 0)
                {
                    query.AppendLine("WHERE AporteId = @AporteId ");
                }

                Transacoes retorno = new Transacoes();

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    if (transacaoId != null)
                        cmd.Parameters.AddWithValue("@TransacaoId", transacaoId);
                    else if (aporteId.Length > 0)
                        cmd.Parameters.AddWithValue("@AporteId", aporteId);
                    
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            retorno.TransacaoId = Convert.ToInt32(dr["TransacaoId"]);
                            retorno.ContaIdOrigem = (dr["ContaIdOrigem"] != DBNull.Value ? Convert.ToInt32(dr["ContaIdOrigem"].ToString()) : default(int?));
                            retorno.ContaIdDestino = Convert.ToInt32(dr["ContaIdDestino"].ToString());
                            retorno.TipoTransacao = Convert.ToInt32(dr["TipoTransacao"]);
                            retorno.Valor = Convert.ToDecimal(dr["Valor"]);
                            retorno.Data = dr["Data"].ToString();
                            retorno.AporteId = dr["AporteId"].ToString();
                        }
                    }
                }

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objCon.conn.Close();
            }
        }
        public string GerarNovoAporteId()
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT AporteId ")
                     .AppendLine("FROM tbTransacoes ")
                     .AppendLine("ORDER BY AporteId DESC LIMIT 1");
                string retorno;
                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    int novoAporteId = 0;
                    retorno = cmd.ExecuteScalar().ToString();
                    int.TryParse(retorno.Replace("APT", ""), out novoAporteId);
                    novoAporteId++;
                    retorno = "APT" + novoAporteId.ToString().PadLeft(6, '0');
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objCon.conn.Close();
            }
        }
    }
}
