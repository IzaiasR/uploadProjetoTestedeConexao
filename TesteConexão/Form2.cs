using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteConexão
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            bunifuFormDock.SubscribeControlToDragEvents(panel);
        }

        string conexao = ConfigurationManager.ConnectionStrings["bd_loja"].ConnectionString;

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CadastrarDados()
        {
            //Capturar os dados das textboxes

            string nome, telefone, email;
            nome = tbx_nome.Text;
            telefone = tbx_tel.Text;
            email = tbx_email.Text;

            //Verificar se as variáveis estão nulas ou vazias

            if (nome == "" || nome == null || telefone == "" || telefone == null || email == "" || email == null)
            {
                MessageBox.Show("  Todos os dados devem ser preenchidos.", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                try
                {
                    //Conexão com o banco de dados

                    MySqlConnection con = new MySqlConnection(conexao);

                    //Montar e executar o comando SQL (Insert into)

                    string sql_insert = @"insert into tb_cliente (tb_cliente_nome, tb_cliente_tel, tb_cliente_email)
                                        values (@cliente_nome, @cliente_tel, @cliente_email)";

                    //Montar e organizar o comando SQL

                    MySqlCommand executacmdMySql_insert = new MySqlCommand(sql_insert, con);
                    executacmdMySql_insert.Parameters.AddWithValue("@cliente_nome", nome);
                    executacmdMySql_insert.Parameters.AddWithValue("@cliente_tel", telefone);
                    executacmdMySql_insert.Parameters.AddWithValue("@cliente_email", email);

                    //Abrir a conexão

                    con.Open();
                    executacmdMySql_insert.ExecuteNonQuery();

                    //Fechar a conexão

                    con.Close();
                    MessageBox.Show("  Cliente cadastrado com sucesso.", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

                catch (Exception erro)
                {
                    MessageBox.Show($"  Ops! Houve um problema. \n \n  Código do erro: {erro.ToString()} ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CarregarDados()
        {
            try
            {
                // 1º passo - Conexão com o banco de dados

                MySqlConnection con = new MySqlConnection(conexao);

                // 2º passo - Montar e executar o comando SQL (insert into)

                string sql_select_cliente = "select * from tb_cliente";

                MySqlCommand executacmdMySql_select = new MySqlCommand(sql_select_cliente, con);

                con.Open();

                executacmdMySql_select.ExecuteNonQuery();

                // 4º passo - Criar um Data Table

                DataTable tabela_cliente = new DataTable();

                // 5º passo - Criar o MySqlDataAdapter

                MySqlDataAdapter da_cliente = new MySqlDataAdapter(executacmdMySql_select);

                da_cliente.Fill(tabela_cliente);

                // 6º passo - Exibir o Data Table no Data Grid View

                grdV_data.DataSource = tabela_cliente;

                con.Clone();

            }

            catch (Exception erro)
            {
                MessageBox.Show($"  Ops! Houve um problema. \n \n  Código do erro: {erro.ToString()} ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AtualizarDados()
        {
            try
            {
                // Declarar as variáveis e inserir os conteúdos dos textos nelas

                int codigo = int.Parse(tbx_code.Text);
                string nome = tbx_nome.Text;
                string tel = tbx_tel.Text;
                string email = tbx_email.Text;

                // Esta classe é utilizada para conectar sua aplicação com o banco de dados MySQL.
                // Através dos métodos desta classe, pode-se realizar algumas operações no banco de dados, como abrir e fechar a conexão, por exemplo.

                MySqlConnection con = new MySqlConnection(conexao);

                con.Open();

                // Variável que receberá o comando SQL

                string sql_update_cliente = @"update tb_cliente set tb_cliente_nome = @nome,
                                                tb_cliente_tel = @tel,
                                                tb_cliente_email = @email
                                                where tb_cliente_id = @id";

                // Organizar e executar o comando SQL

                MySqlCommand executaMySql_update_cliente = new MySqlCommand(sql_update_cliente, con);

                executaMySql_update_cliente.Parameters.AddWithValue("@id", codigo);
                executaMySql_update_cliente.Parameters.AddWithValue("@nome", nome);
                executaMySql_update_cliente.Parameters.AddWithValue("@tel", tel);
                executaMySql_update_cliente.Parameters.AddWithValue("@email", email);

                executaMySql_update_cliente.ExecuteNonQuery();

                MessageBox.Show("  Atualizão realizada com sucesso.", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();

            }

            catch (Exception erro)
            {
                MessageBox.Show($"  Ops! Houve um problema. \n \n  Código do erro: {erro.ToString()} ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcluirDados()
        {
            try
            {
                // Declarar a variável de identificação e inserir o conteúdo do texto nela

                int codigo = int.Parse(tbx_code.Text);

                // Esta classe é utilizada para conectar sua aplicação com o banco de dados MySQL.
                // Através dos métodos desta classe, pode-se realizar algumas operações no banco de dados, como abrir e fechar a conexão, por exemplo.

                MySqlConnection con = new MySqlConnection(conexao);

                con.Open();

                // Variável que receberá o comando SQL

                string sql_delete_cliente = @"delete from tb_cliente where tb_cliente_id = @id";

                // Organizar e executar o comando SQL

                MySqlCommand executaMySql_delete_cliente = new MySqlCommand(sql_delete_cliente, con);

                executaMySql_delete_cliente.Parameters.AddWithValue("@id", codigo);

                executaMySql_delete_cliente.ExecuteNonQuery();

                MessageBox.Show("  Exclusão realizada com sucesso.", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();
            }

            catch (Exception erro)
            {
                MessageBox.Show($"  Ops! Houve um problema. \n \n  Código do erro: {erro.ToString()} ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_listar_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void btn_cad_Click(object sender, EventArgs e)
        {
            CadastrarDados();

            CarregarDados();
        }

        private void btn_alt_Click(object sender, EventArgs e)
        {
            AtualizarDados();

            CarregarDados();
        }

        private void btn_exc_Click(object sender, EventArgs e)
        {
            ExcluirDados();

            CarregarDados();
        }

        private void grdV_data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Coletando os dados de uma linha selecionada no Data Grid View

            tbx_code.Text = grdV_data.CurrentRow.Cells[0].Value.ToString();
            tbx_nome.Text = grdV_data.CurrentRow.Cells[1].Value.ToString();
            tbx_tel.Text = grdV_data.CurrentRow.Cells[2].Value.ToString();
            tbx_email.Text = grdV_data.CurrentRow.Cells[3].Value.ToString();
        }    
    }
}
