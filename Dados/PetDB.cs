using Entidade;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PetDB
    {
        #region Atributos

        private DB conexao;
        StringBuilder sp;

        #endregion

        #region Inserir

        public void InsertPet(Pet pet)
        {
            sp = new StringBuilder();

            sp.Append("INSERT INTO TB_PET (nome, raca) VALUES ");
            sp.Append(string.Format("('{0}', '{1}')", pet.Nome, pet.Raca));

            using (conexao = new DB())
            {
                conexao.ExecutaComando(sp.ToString());
            }
        }

        #endregion

        #region Atualizar

        public void UpdatePet(Pet pet)
        {
            sp = new StringBuilder();

            sp.Append("UPDATE TB_PET SET ");
            sp.Append("nome = '" + pet.Nome + "',");
            sp.Append("raca = '" + pet.Raca + "'");
            sp.Append(" Where codigo = " + pet.Codigo);

            using (conexao = new DB())
            {
                conexao.ExecutaComando(sp.ToString());
            }
        }

        #endregion

        #region Deletar

        public void DeletePet(int id)
        {
            using (conexao = new DB())
            {
                sp = new StringBuilder();

                sp.Append("DELETE FROM TB_PET where codigo = ");
                sp.Append(string.Format("('{0}')", id));

                conexao.ExecutaComando(sp.ToString());
            }
        }

        #endregion

        #region Listar

        public List<Pet> ListarPet()
        {
            using (conexao = new DB())
            {
                var sql = "SELECT codigo, nome, raca FROM TB_PET";
                var retorno = conexao.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno);
            }
        }

        public Pet ListarPetPorID(int id)
        {
            using (conexao = new DB())
            {
                var sql = string.Format("SELECT codigo, nome, raca FROM medicamento WHERE idmedicamento = {0}", id);
                var retorno = conexao.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno).FirstOrDefault();
            }
        }

        private List<Pet> TransformaSQLReaderEmList(SqlDataReader retorno)
        {
            var listPet = new List<Pet>();

            while (retorno.Read())
            {
                var item = new Pet()
                {
                    Codigo = Convert.ToInt32(retorno["codigo"]),
                    Nome = retorno["nome"].ToString(),
                    Raca = retorno["raca"].ToString(),
                };

                listPet.Add(item);
            }
            return listPet;
        }
        #endregion
    }
}
