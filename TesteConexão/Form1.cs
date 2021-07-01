using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Bunifu.UI.WinForms;

namespace TesteConexão
{
    public partial class frm1 : Form
    {
        string conexao = ConfigurationManager.ConnectionStrings["bd_loja"].ConnectionString;

        public frm1()
        {
            InitializeComponent();

            bunifuFormDock1.SubscribeControlToDragEvents(panel1);
            bunifuTransition1.ShowSync(this);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_cadastrar_Click(object sender, EventArgs e)
        {
            
            //Botao cadastrar

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

    }
}
