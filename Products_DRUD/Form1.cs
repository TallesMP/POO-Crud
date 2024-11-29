using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Scheduling_CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (MySqlCommand command = new MySqlCommand("INSERT INTO agendamentos(nome, cpf, nome_servico, data_agendamento) VALUES(@nome, @cpf, @nome_servico, @data_agendamento)", DataBaseConnection.GetConnection()))
            {
                command.Parameters.AddWithValue("@nome", txtName.Text);
                command.Parameters.AddWithValue("@cpf", txtCPF.Text);
                command.Parameters.AddWithValue("@nome_servico", txtServiceName.Text);
                command.Parameters.AddWithValue("@data_agendamento", dtpAgendamento.Value);

                command.ExecuteNonQuery();
                MessageBox.Show("Agendamento Criado com Sucesso!");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM agendamentos WHERE nome LIKE @nome", DataBaseConnection.GetConnection()))
            {
                command.Parameters.AddWithValue("@nome", "%" + txtName.Text + "%");

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["nome"].ToString();
                        txtCPF.Text = reader["cpf"].ToString();
                        txtServiceName.Text = reader["nome_servico"].ToString();
                        dtpAgendamento.Value = Convert.ToDateTime(reader["data_agendamento"]);
                    }
                    else
                    {
                        txtName.Text = "";
                        txtCPF.Text = "";
                        txtServiceName.Text = "";
                        dtpAgendamento.Value = DateTime.Now;
                        MessageBox.Show("Nome do agendamento não encontrado!");
                    }
                }
            }
        }

        private void btn_LoadTable(object sender, EventArgs e)
        {
            string query = "SELECT * FROM agendamentos";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, DataBaseConnection.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGrid.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCPF.Text))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE agendamentos SET nome = @nome, nome_servico = @nome_servico, data_agendamento = @data_agendamento WHERE cpf = @cpf", DataBaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@nome_servico", txtServiceName.Text);
                    cmd.Parameters.AddWithValue("@data_agendamento", dtpAgendamento.Value);
                    cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agendamento atualizado com sucesso!");
                }

                btn_LoadTable(sender, e);
            }
            else
            {
                MessageBox.Show("Por favor, insira o CPF do agendamento para atualizar.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCPF.Text))
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM agendamentos WHERE cpf = @cpf", DataBaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agendamento removido com sucesso!");
                }
                btn_LoadTable(sender, e);
            }
            else
            {
                MessageBox.Show("Por favor, insira o CPF do agendamento para excluir.");
            }
        }
    }
}
