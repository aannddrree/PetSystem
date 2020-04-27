using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetSystem
{
    public partial class PetSystem : Form
    {
        Pet pet;
        PetDB db;

        #region Construtor 

        public PetSystem()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void btncadastrar_Click(object sender, EventArgs e)
        {
            if (VerificaDados(txtnome.Text, cboraca.SelectedItem.ToString()))
            {

                pet = new Pet()
                {
                    Nome = txtnome.Text,
                    Raca = cboraca.SelectedItem.ToString()
                };

                db = new PetDB();
                db.InsertPet(pet);
                Limpar();
                Consultar();
                MessageBox.Show("Registro deletado com sucesso!");
            }
            else
            {
                MessageBox.Show("Favor Preencher todas as informações", "Informação", MessageBoxButtons.OK);
            }
        }

        private void btnalterar_Click(object sender, EventArgs e)
        {
                if (VerificaDados(txtnome.Text, cboraca.SelectedText))
                {
                    pet = new Pet()
                    {
                        Codigo = Convert.ToInt32(txtcodigo.Text),
                        Nome = txtnome.Text,
                        Raca = cboraca.SelectedItem.ToString()
                    };

                    db = new PetDB();
                    db.UpdatePet(pet);
                    Limpar();
                    Consultar();
                    MessageBox.Show("Registro alterado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Favor Preencher todas as informações", "Informação", MessageBoxButtons.OK);
                }
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            int codigo = 0;

            if (!String.IsNullOrEmpty(txtcodigo.Text))
            {
                codigo = Convert.ToInt32(txtcodigo.Text);

                db = new PetDB();
                db.DeletePet(codigo);
                Limpar();
                Consultar();
                MessageBox.Show("Registro excluido com sucesso!");
            }
            else
            {
                MessageBox.Show("Código inválido!");
            }
        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void dgvPet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = (dgvPet.CurrentCell.RowIndex);
            txtcodigo.Text = dgvPet.Rows[rowIndex].Cells[0].Value.ToString();
            txtnome.Text = dgvPet.Rows[rowIndex].Cells[1].Value.ToString();
            cboraca.SelectedItem = dgvPet.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void PetSystem_Activated(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnnovo_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        #endregion

        #region Métodos

        private bool VerificaDados(string nome, string raca)
        {
            pet = new Pet();

            if (String.IsNullOrEmpty(nome) && String.IsNullOrEmpty(raca))
            {
                return false;
            }
            return true;
        }

        private void Consultar()
        {
            db = new PetDB();
            dgvPet.DataSource = db.ListarPet();
        }

        private void Consultar(int id)
        {
            db = new PetDB();
            dgvPet.DataSource = db.ListarPetPorID(id);
        }

        private void Limpar()
        {
            txtcodigo.Clear();
            txtnome.Clear();
            cboraca.SelectedIndex = -1;
            txtnome.Focus();
        }

        #endregion

    }
}
