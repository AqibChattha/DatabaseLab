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
    public partial class ucRegisterStudent : UserControl
    {
        private static ucRegisterStudent _instence;
        private int rowIndex = -1;
        private string regNo = "";
        private string courseName = "";
        private DataGridView dtvTable;
        private bool regNoTable;
        private bool dTable;
        private bool StopUpdate;

        public static ucRegisterStudent Instence
        {
            get
            {
                if (_instence == null)
                {
                    _instence = new ucRegisterStudent();
                }
                return _instence;
            }
        }
        public ucRegisterStudent()
        {
            InitializeComponent();
            dtvTable = ucCRUD.Instence.getDtvTable();
            viewDtvTable();
            dTable = true;
        }

        private void loadStudentRegNos(string regNo = "")
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select RegistrationNumber from Student WHERE RegistrationNumber LIKE '%"+ regNo +"%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtvTable.DataSource = dt;
            regNoTable = true;
            dTable = false;
        }

        private void loadCourseNames(string courseID = "")
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Course_Name from Course WHERE Course_Name LIKE '%" + courseID + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtvTable.DataSource = dt;
            regNoTable = false;
            dTable = false;
        }

        public void refreshUC()
        {
            clearFields();
            viewDtvTable();
        }

        private void tbStudentRegNo_TextChanged(object sender, EventArgs e)
        {
            if (StopUpdate==false)
            {
                loadStudentRegNos(tbStudentRegNo.Text);
            }
        }

        private void tbCourseName_TextChanged(object sender, EventArgs e)
        {
            if (StopUpdate == false)
            {
                loadCourseNames(tbCourseName.Text);
            }
        }

        public void updateIndex_RegNo(int rowI)
        {
            rowIndex = rowI;
            if (dTable == true)
            {
                StopUpdate = true;
                tbStudentRegNo.Text = dtvTable.Rows[rowIndex].Cells[0].Value.ToString();
                tbCourseName.Text = dtvTable.Rows[rowIndex].Cells[1].Value.ToString();
                StopUpdate = false;
            }
            else if (regNoTable == true)
            {
                tbStudentRegNo.Text = dtvTable.Rows[rowIndex].Cells[0].Value.ToString();
            }
            else
            {
                tbCourseName.Text = dtvTable.Rows[rowIndex].Cells[0].Value.ToString();
            }
            regNo = tbStudentRegNo.Text;
            courseName = tbCourseName.Text;
        }

        private void clearFields()
        {
            tbStudentRegNo.Text = "";
            tbCourseName.Text = "";
            regNo = "";
            rowIndex = -1;
        }

        private void viewDtvTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Enrollments", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtvTable.DataSource = dt;
            dTable = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand check_RegNo = new SqlCommand("SELECT COUNT(*) FROM Student WHERE (RegistrationNumber = @RegistrationNumber)", con);
            check_RegNo.Parameters.AddWithValue("@RegistrationNumber", tbStudentRegNo.Text);
            int RegNoExist = (int)check_RegNo.ExecuteScalar();

            SqlCommand check_CourseName = new SqlCommand("SELECT COUNT(*) FROM Course WHERE (Course_Name = @Course_Name)", con);
            check_CourseName.Parameters.AddWithValue("@Course_Name", tbCourseName.Text);
            int CourseNameExist = (int)check_CourseName.ExecuteScalar();

            if (RegNoExist > 0 && CourseNameExist > 0)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Insert into Enrollments values (@StudentRegNo, @CourseName)", con);
                    cmd.Parameters.AddWithValue("@StudentRegNo", tbStudentRegNo.Text);
                    cmd.Parameters.AddWithValue("@CourseName", tbCourseName.Text);
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
            else
            {
                MessageBox.Show("Invalid input please try again.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbStudentRegNo.Text != "" && tbCourseName.Text != "")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Enrollments WHERE StudentRegNo = '" + tbStudentRegNo.Text + "' and CourseName = '"+ tbCourseName.Text + "'", con))
                    {
                        command.ExecuteNonQuery();
                        clearFields();
                        viewDtvTable();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid input.");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rowIndex >= 0 && regNo != "" && courseName != "")
            {
                if (dtvTable.Rows[rowIndex].Cells[0].Value.ToString() != tbStudentRegNo.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Enrollments SET StudentRegNo = '" + tbStudentRegNo.Text + "' WHERE StudentRegNo = '" + regNo + "' and CourseName = '" + courseName + "'", con))
                        {
                            command.ExecuteNonQuery();
                            regNo = tbStudentRegNo.Text;
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[1].Value.ToString() != tbCourseName.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Enrollments SET CourseName = '" + tbCourseName.Text + "' WHERE StudentRegNo = '" + regNo + "' and CourseName = '" + courseName + "'", con))
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

        public void loadSeachedInDtv(string txt)
        {
            var con = Configuration.getInstance().getConnection();
            if (dTable == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from Enrollments " +
                        "WHERE StudentRegNo LIKE '%" + txt + "%' " +
                        "or CourseName like '%" + txt + "%'", con);
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
            else if (regNoTable == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select StudentRegNo from Enrollments " +
                        "WHERE StudentRegNo LIKE '%" + txt + "%'", con);
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
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select CourseName from Enrollments " +
                        "where CourseName like '%" + txt + "%'", con);
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

        private void btnEnrollments_Click(object sender, EventArgs e)
        {
            viewDtvTable();
        }
    }
}
