﻿using AppLayoutAspCore.Models;
using AppLayoutAspCore.Repository.Contrato;
using MySql.Data.MySqlClient;

namespace AppLayoutAspCore.Repository
{
    // Extends IEmprestimoRepository
    public class EmprestimoRepository : IEmprestimoRepository
    {

        private readonly string _conexaoMySQL;
        // construtor com paramentro injeção da conexao do banco
        public EmprestimoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
               
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Emprestimo(Id, dataEmp,dataDev) values(@Id, @dtEmpre, @dtDev )", conexao);

                cmd.Parameters.Add("@dtEmpre", MySqlDbType.VarChar).Value = emprestimo.dtEmpre;
                cmd.Parameters.Add("@dtDev", MySqlDbType.VarChar).Value = emprestimo.dtDev;
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = emprestimo.codUsu;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        //selecionar o ultimo emprestimo inserido 
        public void buscaIdEmp(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlDataReader dr;
                MySqlCommand cmd = new MySqlCommand("SELECT codEmp FROM Emprestimo ORDER BY codEmp DESC limit 1", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emprestimo.codEmp = dr[0].ToString();
                }
                conexao.Close();
            }
        }


        public void Atualizar(Emprestimo emprestimo)
        {
            throw new NotImplementedException();
        }


        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }
        public Emprestimo ObterEmprestimos(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Emprestimo> ObterTodosEmprestimos()
        {
            throw new NotImplementedException();
        }
    }
}
