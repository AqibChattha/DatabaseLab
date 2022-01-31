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

namespace Lab2_Home
{
    public partial class ucStudent : UserControl
    {
        private static ucStudent _instence;
        public static ucStudent Instence
        {
            get
                {
                    if (_instence==null)
                    {
                        _instence = new ucStudent();
                    }
                    return _instence;
                }
        }

        private int rowIndex = -1;
        private string regNo = "";
        private string stdName = "";
        private DataGridView dtvTable;

        private ucStudent()
        {
            InitializeComponent();
            dtvTable = ucCRUD.Instence.getDtvTable();
            viewDtvTable();
        }

        public void updateIndex_RegNo(int rowI)
        {
            rowIndex = rowI;
            tbRegNo.Text = dtvTable.Rows[rowIndex].Cells[0].Value.ToString();
            tbName.Text = dtvTable.Rows[rowIndex].Cells[1].Value.ToString();
            tbDepartment.Text = dtvTable.Rows[rowIndex].Cells[2].Value.ToString();
            tbSession.Text = dtvTable.Rows[rowIndex].Cells[3].Value.ToString();
            tbAddress.Text = dtvTable.Rows[rowIndex].Cells[4].Value.ToString();
            regNo = tbRegNo.Text;
            stdName = tbName.Text;
        }
        
        private void clearFields()
        {
            tbRegNo.Text = "";
            tbName.Text = "";
            tbDepartment.Text = "";
            tbSession.Text = "";
            tbAddress.Text = "";
            regNo = "";
            rowIndex = -1;
        }

        private void viewDtvTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtvTable.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Student values (@RegistrationNumber, @Name, @Department, @Session, @Address)", con);
                cmd.Parameters.AddWithValue("@RegistrationNumber", tbRegNo.Text);
                cmd.Parameters.AddWithValue("@Name", tbName.Text);
                cmd.Parameters.AddWithValue("@Department", tbDepartment.Text);
                cmd.Parameters.AddWithValue("@Session", int.Parse(tbSession.Text));
                cmd.Parameters.AddWithValue("@Address", tbAddress.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                clearFields();
                viewDtvTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbRegNo.Text != "")
            {
                try
                {

                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid input.");
                }
                var con = Configuration.getInstance().getConnection();
                using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE RegistrationNumber = '" + tbRegNo.Text + "' and Name = '" + stdName + "'", con))
                {
                    command.ExecuteNonQuery();
                    clearFields();
                    viewDtvTable();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rowIndex != -1 && regNo != "")
            {
                if (dtvTable.Rows[rowIndex].Cells[0].Value.ToString() != tbRegNo.Text)
                {
                    try
                    {

                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Student SET RegistrationNumber = '" + tbRegNo.Text + "' WHERE RegistrationNumber = '" + regNo + "' and Name = '" + stdName + "'", con))
                        {
                            command.ExecuteNonQuery();
                            regNo = tbRegNo.Text;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[1].Value.ToString() != tbName.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Student SET Name = '" + tbName.Text + "' WHERE RegistrationNumber = '" + regNo + "' and Name = '" + stdName + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[2].Value.ToString() != tbDepartment.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Student SET Department = '" + tbDepartment.Text + "' WHERE RegistrationNumber = '" + regNo + "' and Name = '" + stdName + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[3].Value.ToString() != tbSession.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Student SET Session = '" + tbSession.Text + "' WHERE RegistrationNumber = '" + regNo + "' and Name = '" + stdName + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[4].Value.ToString() != tbAddress.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Student SET Address = '" + tbAddress.Text + "' WHERE RegistrationNumber = '" + regNo + "' and Name = '" + stdName + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                clearFields();
                viewDtvTable();
            }
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        public void refreshUC()
        {
            clearFields();
            viewDtvTable();
        }

        public void loadSeachedInDtv(string txt)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select * from Student " +
                    "WHERE RegistrationNumber LIKE '%" + txt + "%' " +
                    "or Name like '%" + txt + "%' " +
                    "or Department like '%" + txt + "%' " +
                    "or Session like '%" + txt + "%' " +
                    "or Address like '%" + txt + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtvTable.DataSource = dt;

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input.");
            }
        }
    }
}
