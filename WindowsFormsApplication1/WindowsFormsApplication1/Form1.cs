using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private string regNo = "";
        private int rowIndex = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Student values (@RegistrationNumber, @Name, @Department, @Session, @CGPA, @Address)", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Department", textBox3.Text);
            cmd.Parameters.AddWithValue("@Session", int.Parse(textBox4.Text));
            cmd.Parameters.AddWithValue("@CGPA", Convert.ToDouble(textBox5.Text));
            cmd.Parameters.AddWithValue("@Address", textBox6.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE RegistrationNumber = '" + textBox1.Text + "'", con))
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                regNo = textBox1.Text;
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rowIndex != -1 && regNo != "")
            {
                if (dataGridView1.Rows[rowIndex].Cells[0].Value.ToString() != textBox1.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET RegistrationNumber = '" + textBox1.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                if (dataGridView1.Rows[rowIndex].Cells[1].Value.ToString() != textBox2.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET Name = '" + textBox2.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                if (dataGridView1.Rows[rowIndex].Cells[2].Value.ToString() != textBox3.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET Department = '" + textBox3.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                if (dataGridView1.Rows[rowIndex].Cells[3].Value.ToString() != textBox4.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET Session = '" + textBox4.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                if (dataGridView1.Rows[rowIndex].Cells[4].Value.ToString() != textBox5.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET CGPA = '" + textBox5.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                if (dataGridView1.Rows[rowIndex].Cells[5].Value.ToString() != textBox6.Text)
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("UPDATE Student SET Address = '" + textBox6.Text + "' WHERE RegistrationNumber = '" + regNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
