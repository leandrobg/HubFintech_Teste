using System;
using System.Data.SQLite;
using System.Text;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Data
{
    public class ContasData
    {
        ConnectionFactory objCon = new ConnectionFactory();

        public void Incluir(Contas c)
        {
            try
            {
                objCon.conn.Open();

                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT ContaIdMatriz FROM tbContas WHERE ContaId = @ContaIdPai");
                int? matrizId = null;

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@ContaIdPai", c.ContaIdPai);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                        matrizId = Convert.ToInt32(resultado);

                    if (matrizId == null || matrizId.Value == 0)
                        matrizId = (c.ContaIdPai == 0 ? null : c.ContaIdPai);
                }

                query.Clear();

                query.AppendLine("INSERT INTO tbContas (Nome, DataCriacao, ContaIdPai, Situacao, PessoaId, ContaIdMatriz) ")
                 .AppendLine("VALUES(@Nome, DATE('now'), @ContaIdPai, @Situacao, @PessoaId, @ContaIdMatriz) ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", c.Nome);
                    cmd.Parameters.AddWithValue("@ContaIdPai", (c.ContaIdPai == 0 ? null : c.ContaIdPai));
                    cmd.Parameters.AddWithValue("@Situacao", c.Situacao);
                    cmd.Parameters.AddWithValue("@PessoaId", c.PessoaId);
                    cmd.Parameters.AddWithValue("@ContaIdMatriz", matrizId);
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
                {
                    objCon.conn.Close();
                }
            }
        }
        public void Alterar(Contas c)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE tbContas ")
                     .AppendLine("SET Nome = @Nome ")
                     .AppendLine("   ,Situacao = @Situacao ")
                     .AppendLine("   ,PessoaId = @PessoaId ")
                     .AppendLine("WHERE ContaId = @ContaId ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@ContaId", c.ContaId);
                    cmd.Parameters.AddWithValue("@Nome", c.Nome);
                    cmd.Parameters.AddWithValue("@Situacao", c.Situacao);
                    cmd.Parameters.AddWithValue("@PessoaId", c.PessoaId);
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
                {
                    objCon.conn.Close();
                }
            }
        }
        public int Excluir(int contaId)
        {
            try
            {
                objCon.conn.Open();
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM tbContas WHERE ContaId = @ContaId ");

                using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
                {
                    cmd.Parameters.AddWithValue("@ContaId", contaId);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (objCon.conn.State == System.Data.ConnectionState.Open)
                {
                    objCon.conn.Close();
                }
            }
        }
        public Contas Consultar(int contaId)
        {
            objCon.conn.Open();
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT ContaId, Nome, DataCriacao, ContaIdPai, Situacao, PessoaId, ContaIdMatriz")
                 .AppendLine("FROM tbContas ")
                 .AppendLine("WHERE ContaId = @ContaId ");

            Contas retorno = new Contas();

            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
            {
                cmd.Parameters.AddWithValue("@ContaId", contaId);
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retorno.ContaId = Convert.ToInt32(dr["ContaId"]);
                        retorno.Nome = dr["Nome"].ToString();
                        retorno.DataCriacao = dr["DataCriacao"].ToString();
                        retorno.Situacao = Convert.ToInt32(dr["Situacao"]);
                        retorno.PessoaId = Convert.ToInt32(dr["PessoaId"]);

                        if (dr["ContaIdMatriz"] != DBNull.Value)
                        {
                            retorno.ContaIdMatriz = Convert.ToInt32(dr["ContaIdMatriz"]);
                        }

                        if (dr["ContaIdPai"] != DBNull.Value)
                        {
                            retorno.ContaIdPai = Convert.ToInt32(dr["ContaIdPai"]);
                        }
                    }
                }
            }
            objCon.conn.Close();

            return retorno;
        }
    }
}
