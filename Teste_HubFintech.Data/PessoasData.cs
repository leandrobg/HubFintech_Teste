using System;
using System.Data.SQLite;
using System.Text;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Data
{
    public class PessoasData
    {
        ConnectionFactory objCon = new ConnectionFactory();
        public void Incluir(Pessoas p)
        {
            objCon.conn.Open();
            StringBuilder query = new StringBuilder();
            query.AppendLine("INSERT INTO tbPessoas (CpfCnpj, NomeCompleto, DataNascimento, RazaoSocial, NomeFantasia) ")
                 .AppendLine("VALUES(@CpfCnpj, @NomeCompleto, @DataNascimento, @RazaoSocial, @NomeFantasia) ");

            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
            {
                cmd.Parameters.AddWithValue("@CpfCnpj", p.CpfCnpj);
                cmd.Parameters.AddWithValue("@NomeCompleto", p.NomeCompleto);
                cmd.Parameters.AddWithValue("@DataNascimento", p.DataNascimento);
                cmd.Parameters.AddWithValue("@RazaoSocial", p.RazaoSocial);
                cmd.Parameters.AddWithValue("@NomeFantasia", p.NomeFantasia);
                cmd.ExecuteNonQuery();
            }
            objCon.conn.Close();
        }

        public void Alterar(Pessoas p)
        {
            objCon.conn.Open();
            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE tbPessoas ")
                 .AppendLine("SET CpfCnpj = @CpfCnpj ")
                 .AppendLine("   ,NomeCompleto = @NomeCompleto ")
                 .AppendLine("   ,DataNascimento = @DataNascimento ")
                 .AppendLine("   ,RazaoSocial = @RazaoSocial ")
                 .AppendLine("   ,NomeFantasia = @NomeFantasia ")
                 .AppendLine("WHERE PessoaId = @PessoaId ") ;

            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
            {
                cmd.Parameters.AddWithValue("@PessoaId", p.PessoaId);
                cmd.Parameters.AddWithValue("@CpfCnpj", p.CpfCnpj);
                cmd.Parameters.AddWithValue("@NomeCompleto", p.NomeCompleto);
                cmd.Parameters.AddWithValue("@DataNascimento", p.DataNascimento);
                cmd.Parameters.AddWithValue("@RazaoSocial", p.RazaoSocial);
                cmd.Parameters.AddWithValue("@NomeFantasia", p.NomeFantasia);
                cmd.ExecuteNonQuery();
            }
            objCon.conn.Close();
        }
        public int Excluir(int pessoaId)
        {
            objCon.conn.Open();
            StringBuilder query = new StringBuilder();
            query.AppendLine("DELETE FROM tbPessoas WHERE PessoaId = @PessoaId ");

            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
            {
                cmd.Parameters.AddWithValue("@PessoaId", pessoaId);
                return cmd.ExecuteNonQuery();
            }
            objCon.conn.Close();
        }
        public Pessoas Consultar(int pessoaId)
        {
            objCon.conn.Open();
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT PessoaId, CpfCnpj, NomeCompleto, DataNascimento, RazaoSocial, NomeFantasia ")
                 .AppendLine("FROM tbPessoas ")
                 .AppendLine("WHERE PessoaId = @PessoaId ");

            Pessoas retorno = new Pessoas();

            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), objCon.conn))
            {
                cmd.Parameters.AddWithValue("@PessoaId", pessoaId);
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retorno.PessoaId = Convert.ToInt32(dr["PessoaId"]);
                        retorno.CpfCnpj = dr["CpfCnpj"].ToString();
                        retorno.NomeCompleto = dr["NomeCompleto"].ToString();
                        retorno.DataNascimento = dr["DataNascimento"].ToString();
                        retorno.RazaoSocial = dr["RazaoSocial"].ToString();
                        retorno.NomeFantasia = dr["NomeFantasia"].ToString();
                    }
                }
            }
            objCon.conn.Close();

            return retorno;
        }
    }
}
