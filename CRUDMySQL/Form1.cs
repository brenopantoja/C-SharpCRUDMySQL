using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;// It has using DataBase MySQL

/* Command of the Data Base: MySQl:
  * 
 create database agenda;

use agenda;

create table contatos(
id int auto_increment,
nome varchar(150),
email varchar(150),
telefone varchar(12),
primary key(id)
);
 */

namespace CRUDMySQL
{
    public partial class Form1 : Form
    {
        private MySqlConnection conection;
        private string data_source = "datasource=localhost; username=root; password=; database=agenda";

        private int ?idContat_Select = null;// Type anull: It has taking any value or varilbe type

        public Form1()
        {
            InitializeComponent();
            // It has showing column of form good:  Data of the Data Base, width, position in List View, 

            /*
            idContat_Select = null;
            idContat_Select = 23;*/

            listViewContatos.View = View.Details;
            listViewContatos.LabelEdit = true;
            listViewContatos.AllowColumnReorder = true;
            listViewContatos.FullRowSelect = true;
            listViewContatos.GridLines = true;

            listViewContatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            listViewContatos.Columns.Add("Nome", 130, HorizontalAlignment.Left);
            listViewContatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            listViewContatos.Columns.Add("Telefone", 90, HorizontalAlignment.Left);

            load_contact();


        }

        private void load_contact()
        {
            try
            {
                conection = new MySqlConnection(data_source);
                conection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection;

                  cmd.CommandText = " SELECT * FROM contatos ORDER BY id DESC";
               // cmd.CommandText = " SELECT * FROM contatos WHERE nome LIKE @q OR email LIKE @q";

                //cmd.CommandText = " SELECT * FROM contatos";


                // cmd.Parameters.AddWithValue("@q", "%" + textBoxBuscar.Text + "%");
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();



                listViewContatos.Items.Clear();

                while (reader.Read())
                {// It has taking all of the Data Base
                    string[] rowlist = {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };
                    // var rowlistView = new ListViewItem(rowlist);
                    listViewContatos.Items.Add(new ListViewItem(rowlist));
                }
            }
            catch (MySqlException ex)
            // catch (MySqlException ex)
            {

                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show("Error ", +ex.Number + "Ocorreu: " + ex.Message, MessageBoxButtons.OK,
                //  MessageBoxIcon.Error);
                // MessageBox.Show(ex.Message);

            }

            catch (Exception ex)
            {
                MessageBox.Show(" Ocorreu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                conection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {


            /* MySqlCommand cmd = new MySqlCommand();



             cmd.CommandText = "INSERT  INTO contatos (nome, telefone, email)" +
                 "VALUES" + "(@nome, @telefone, @email)";
             MySqlCommand conection = new MySqlCommand(data_source);

             cmd.Connection = conection;

             //  MySqlCommand command = new MySqlCommand(sql, conection);

             cmd.Parameters.AddWithValue("@Nome", textName.Text);
             cmd.Parameters.AddWithValue("@telefone", textTelefone.Text);
             cmd.Parameters.AddWithValue("@email", textEmail.Text);

             cmd.CommandType = CommandType.Text;
             conection.Open();

             try
             {
                 int i = cmd.ExecuteNonQuery();
                 if (i > 0)
                     MessageBox.Show("Registro incluido com sucesso!");
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Erro: " + ex.ToString());
             }
             finally
             {
                 conection.Close();
             }
         }

         */

            try
            {

                //It has working stammentd

                //It has connection with the Data Base

                conection = new MySqlConnection(data_source);
                //It has working MySQL staments




                conection.Open();

                MySqlCommand cmd = new MySqlCommand(null, conection);
                cmd.Connection = conection;
                //  cmd.Connection = conection;
                // It has calling the SQL Command

              //  MessageBox.Show("Id selecionado:" + idContat_Select);

                if (idContat_Select == null)
                {
                    //Insert

            
                    //cmd.CommandText = "INSERT INTO contatos (nome, email,telefone) " +
                    //"VALUES (@nome, @email, @telefone)";



                    //It has preparing the field of the View
                    cmd.Parameters.AddWithValue("@nome", textName.Text);

                    cmd.Parameters.AddWithValue("@email", textEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", textTelefone.Text);
                    cmd.Prepare();

                    //cmd.ExecuteNonQuery();//It has executing the insert in Data Base

                 

                   // MessageBox.Show("Dados inseridos na Data Base !", "Sucesso..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
                    cmd.CommandText = "INSERT INTO contatos (nome, email,telefone) " +
                    "VALUES (@nome, @email, @telefone)";

                    cmd.ExecuteNonQuery();//It has executing the insert in Data Base
                    /*
                    //It has preparing the field of the View
                    cmd.Parameters.AddWithValue("@nome", textName.Text);

                    cmd.Parameters.AddWithValue("@email", textEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", textTelefone.Text);*/
                    //                    cmd.Prepare();

                    //cmd.ExecuteNonQuery();//It has executing the insert in Data Base



                    MessageBox.Show("Dados inseridos na Data Base !", "Sucesso..", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    load_contact();
                    clearButtons();//It has doing clear buttons

                }

            
                else { //Update 

                    cmd.CommandText = "UPDATE contatos SET" +
                        " nome=@nome, email=@email,telefone=@telefone " +
                    "WHERE id=@id ";



                    //It has preparing the field of the View
                    cmd.Parameters.AddWithValue("@nome", textName.Text);

                    cmd.Parameters.AddWithValue("@email", textEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", textTelefone.Text);
                    cmd.Parameters.AddWithValue("@id", idContat_Select);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();//It has executing the Update in Data Base



                    MessageBox.Show("Dados Atualizado na Data Base !", "Sucesso..", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

                /*

                //MySqlCommand cmd = new MySqlCommand( "INSERT INTO contatos VALUES(NULL,@nome, @email, @telefone) " , conection);
                cmd.CommandText = "INSERT INTO contatos (nome, email,telefone) " +
                //  "VALUES " +
                //"(@nome, @email, @telefone)";
                "VALUES (@nome, @email, @telefone)";



                /* Example:
                 * https://zetcode.com/csharp/mysql/
                 * ar sql = "INSERT INTO cars(name, price) VALUES(@name, @price)";
                    using var cmd = new MySqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@name", "BMW");
                    cmd.Parameters.AddWithValue("@price", 36600);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                 * cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
                    cmd.ExecuteNonQuery();*/



                //It has preparing the field of the View
                /*
                cmd.Parameters.AddWithValue("@nome", textName.Text);

                cmd.Parameters.AddWithValue("@email", textEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", textTelefone.Text);
                cmd.Prepare();

                cmd.ExecuteNonQuery();//It has executing the insert in Data Base

                /* string sql = "INSERT INTO contatos (nome, email,telefone) " +
                     "VALUES" +
                     "('" + textName.Text + "', '" + textEmail.Text + "','" + textTelefone.Text + "')";

                */
                // cmd.ExecuteNonQuery();//It has executing the insert in Data Base

                // It has insering in Data Base
                //  MySqlCommand command = new MySqlCommand(sql, conection);
                //command.ExecuteReader();
                //conection.Open();
                //    cmd.ExecuteNonQuery();//It has executing the insert in Data Base

                /*
                MessageBox.Show("Dados inseridos na Data Base !", "Sucesso..", MessageBoxButtons.OK, MessageBoxIcon.Information);

                load_contact();
                clearButtons();//It has doing clear buttons */
            }

            catch (MySqlException ex)
            
            {

                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         

            }

            catch (Exception ex)
            {
                MessageBox.Show(" Ocorreu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                conection.Close();
            }

        }


        private void clearButtons() {
            textName.Focus();
            textName.Text = string.Empty;
            textEmail.Text = String.Empty;
            textTelefone.Text = string.Empty;

            buttonDelete.Visible = false;// Button delete has staying hide
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            idContat_Select= null;
            clearButtons();

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                conection = new MySqlConnection(data_source);
                conection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection;

                //   cmd.CommandText = " SELECT * FROM contatos ORDER BY id DESC";
                cmd.CommandText = " SELECT * FROM contatos WHERE nome LIKE @q OR email LIKE @q";

                


                 cmd.Parameters.AddWithValue("@q", "%" + textBoxBuscar.Text + "%");
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();



                listViewContatos.Items.Clear();

                while (reader.Read())
                {// It has taking all of the Data Base
                    string[] rowlist = {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };
                    // var rowlistView = new ListViewItem(rowlist);
                    listViewContatos.Items.Add(new ListViewItem(rowlist));
                }
            }
            catch (MySqlException ex)
            // catch (MySqlException ex)
            {

                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show("Error ", +ex.Number + "Ocorreu: " + ex.Message, MessageBoxButtons.OK,
                //  MessageBoxIcon.Error);
                // MessageBox.Show(ex.Message);

            }

            catch (Exception ex)
            {
                MessageBox.Show(" Ocorreu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                conection.Close();
            }

            /* }

           try
           { //It has taking data of the Data Base

               string qBuscar = "'%" + textBoxBuscar.Text + "%'";

               //select * from contatos where  nome '%Breno%'

               conection = new MySqlConnection(data_source);
               string sql = "SELECT *" +
                   " FROM  contatos " +
                   "WHERE nome LIKE " + qBuscar + "OR email LIKE" + qBuscar;
               // It has ope connetion in Data Base
               conection.Open();


               MySqlCommand command = new MySqlCommand(sql, conection);

              MySqlDataReader reader =    command.ExecuteReader();
               listViewContatos.Items.Clear(); //It has clearing listView of the screen

               while (reader.Read()) { //The loop to execute and read the data
                   // The row has showing in screen
                   string[] rowlist =
                       {
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetString(2),
                       reader.GetString(3),

                       };
                   var rowlistView = new ListViewItem(rowlist);
                   listViewContatos.Items.Add(rowlistView);
               }



           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
           finally {
               conection.Close();
           }*/

        }


        private void buttonLimparBusca_Click(object sender, EventArgs e)
        {
            textBoxBuscar.Clear();
        }

        private void listViewContatos_SelectedIndexChanged(object sender, EventArgs e)
        {// It has taking each of the selection
            ListView.SelectedListViewItemCollection items_select= listViewContatos.SelectedItems;
;
                foreach (ListViewItem item in items_select) {// Foreach has taking each item selection and it has showing in buttons

                idContat_Select = Convert.ToInt32(item.SubItems[0].Text);

                textName.Text = item.SubItems[1].Text;
                textEmail.Text = item.SubItems[2].Text;
                textTelefone.Text = item.SubItems[3].Text;

                buttonDelete.Visible = true;// Button delete has staying show

                //MessageBox.Show("Id selecionado:" + idContat_Select);
            }

          
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            //It has deleting on Data Base
            deleteContact();
           
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            deleteContact();
        }

        private void deleteContact() {

            try
            {
                // It has send message to user
                DialogResult confirm = MessageBox.Show("tem certeza deseja excluir o contato ?",
                    "Tem certeza?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {

                    conection = new MySqlConnection(data_source);
                    //It has working MySQL staments


                    conection.Open();

                    MySqlCommand cmd = new MySqlCommand(null, conection);
                    cmd.Connection = conection;



                    cmd.CommandText = "DELETE FROM contatos WHERE id=@id";



                    cmd.Parameters.AddWithValue("@id", idContat_Select);
                    cmd.ExecuteNonQuery();//It has executing the insert in Data Base
                    cmd.Prepare();


                    //It has connection with the Data Base



                    MessageBox.Show("Contato excluido !", "Com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    load_contact();
                    clearButtons();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Ocorreu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            //MessageBox.Show("Ira excluir contato: " + idContat_Select);

        }

    }


}
