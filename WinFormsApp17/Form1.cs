using MySql.Data.MySqlClient;
using System.Data;

namespace WinFormsApp17
{
    public partial class FormPrincipal : Form
    {
        Database db = new Database();

        public FormPrincipal()
        {

            InitializeComponent();
            LoadData();
        }

        // Carregar produtos no DataGridView
        private void LoadData()
        {
            db.OpenConnection();
            string query = "SELECT * FROM Produto";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.GetConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            db.CloseConnection();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            FormCadastro formCadastro = new FormCadastro();
            if (formCadastro.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Recarrega os dados ap�s adicionar
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                string nome = dataGridView1.SelectedRows[0].Cells["Nome"].Value.ToString();
                decimal preco = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Preco"].Value);

                FormCadastro formCadastro = new FormCadastro(id, nome, preco);
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Recarrega os dados ap�s atualizar
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                db.OpenConnection();
                string query = "DELETE FROM Produto WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                db.CloseConnection();
                LoadData(); // Recarrega os dados ap�s excluir
            }
            else
            {
                MessageBox.Show("Selecione um produto para excluir.");
            }
        }
    }
}
