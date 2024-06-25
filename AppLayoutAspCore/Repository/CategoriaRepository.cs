
using AppLayoutAspCore.Models;
using AppLayoutAspCore.Repositories.Contracts;

using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//using X.PagedList;

namespace AppLayoutAspCore.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        // Propriedade Privada para injetar a conexão com o banco de dados ;
        private readonly string _conexaoMySQL;

        //Metodo construtor da classe CategoriaRepository    
        public CategoriaRepository(IConfiguration conf)
        {
            // Injeção de dependencia do banco de dados
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Categoria ObterCategoria(int Id)
        {
            throw new NotImplementedException();
        }

        public Categoria ObterCategoria(string Slug)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> ObterCategoriasRecursivas(Categoria categoriaPai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> ObterTodasCategorias()
        {
            List<Categoria> catList = new List<Categoria>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Categoria", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    catList.Add(
                        new Categoria
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            

                        });
                }
                return catList;
            }
        }
    }
}
