using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Loja
{
    internal class ProdutoDAO: IDisposable
    {
        private IDbConnection conexao;
        
        public ProdutoDAO()
        {
            this.conexao = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            this.conexao.Open();
        }

        public void Dispose()
        {
            this.conexao.Close();
        }

        internal void Adicionar(Produto p)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Produtos (Nome, Categoria, Preco) VALUES (@nome, @categoria, @preco)";

                IDbDataParameter paramNome = new SqlParameter("nome", p.Nome);
                insertCmd.Parameters.Add(paramNome);

                IDbDataParameter paramCategoria = new SqlParameter("categoria", p.Categoria);
                insertCmd.Parameters.Add(paramCategoria);

                IDbDataParameter paramPreco = new SqlParameter("preco", p.Preco);
                insertCmd.Parameters.Add(paramPreco);

                insertCmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        internal void Atualizar(Produto p)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "Update Produtos SET nome = @nome, categoria = @categoria, preco = @preco ";

                IDbDataParameter paramNome = new SqlParameter("nome", p.Nome);
                IDbDataParameter paramCategoria = new SqlParameter("nome", p.Categoria);
                IDbDataParameter paramPreco = new SqlParameter("nome", p.Preco);
                IDbDataParameter paramId = new SqlParameter("nome", p.ID);

                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramCategoria);
                updateCmd.Parameters.Add(paramPreco);
                updateCmd.Parameters.Add(paramId);

                updateCmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {

                throw new SystemException(e.Message,e);
            }
        }


        internal void Remover(Produto p)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "Delete from Produtos where id=@id ";
                IDbDataParameter paramId = new SqlParameter("id", p.ID);
                deleteCmd.Parameters.Add(paramId);
                deleteCmd.ExecuteNonQuery();                
            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        internal IList<Produto> Produtos()
        {
            var lista = new List<Produto>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "Select * from Produtos";
            
            IDataReader resultado = selectCmd.ExecuteReader();

            while (resultado.Read())
            {
                Produto p = new Produto();
                p.ID = Convert.ToInt32(resultado["Id"]);
                p.Nome = Convert.ToString(resultado["Nome"]);
                p.Categoria = Convert.ToString(resultado["Categoria"]);
                p.Preco = Convert.ToDouble(resultado["Preco"]);

                lista.Add(p);
            }

            return lista;
        }



    }
}
