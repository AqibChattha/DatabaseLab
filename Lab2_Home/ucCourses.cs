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
    public partial class ucCourses : UserControl
    {
        private static ucCourses _instence;
        public static ucCourses Instence
        {
            get
            {
                if (_instence == null)
                {
                    _instence = new ucCourses();
                }
                return _instence;
            }
        }

        private int rowIndex = -1;
        private string regNo = "";
        private DataGridView dtvTable;

        private ucCourses()
        {
            InitializeComponent();
            dtvTable = ucCRUD.Instence.getDtvTable();
            viewDtvTable();
        }

        public void updateIndex_RegNo(int rowI)
        {
            rowIndex = rowI;
            tbCourseID.Text = dtvTable.Rows[rowIndex].Cells[0].Value.ToString();
            tbCourseName.Text = dtvTable.Rows[rowIndex].Cells[1].Value.ToString();
            tbStudentName.Text = dtvTable.Rows[rowIndex].Cells[2].Value.ToString();
            tbTeacherName.Text = dtvTable.Rows[rowIndex].Cells[3].Value.ToString();
            tbSemester.Text = dtvTable.Rows[rowIndex].Cells[4].Value.ToString();
            regNo = tbCourseID.Text;
        }

        private void clearFields()
        {
            tbCourseID.Text = "";
            tbCourseName.Text = "";
            tbStudentName.Text = "";
            tbTeacherName.Text = "";
            tbSemester.Text = "";
            regNo = "";
            rowIndex = -1;
        }

        private void viewDtvTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course", con);
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
                SqlCommand cmd = new SqlCommand("Insert into Course values (@Course_ID, @Course_Name, @Student_Name, @Teacher_Name, @Semester)", con);
                cmd.Parameters.AddWithValue("@Course_ID", tbCourseID.Text);
                cmd.Parameters.AddWithValue("@Course_Name", tbCourseName.Text);
                cmd.Parameters.AddWithValue("@Student_Name", tbStudentName.Text);
                cmd.Parameters.AddWithValue("@Teacher_Name", tbTeacherName.Text);
                cmd.Parameters.AddWithValue("@Semester", tbSemester.Text);
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

        private void btnView_Click(object sender, EventArgs e)
        {
            viewDtvTable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbCourseID.Text != "")
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Course WHERE Course_ID = '" + tbCourseID.Text + "'", con))
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
            if (rowIndex != -1 && regNo != "")
            {
                if (dtvTable.Rows[rowIndex].Cells[0].Value.ToString() != tbCourseID.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Course SET Course_ID = '" + tbCourseID.Text + "' WHERE Course_ID = '" + regNo + "'", con))
                        {
                            command.ExecuteNonQuery();
                            regNo = tbCourseID.Text;
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
                        using (SqlCommand command = new SqlCommand("UPDATE Course SET Course_Name = '" + tbCourseName.Text + "' WHERE Course_ID = '" + regNo + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[2].Value.ToString() != tbStudentName.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Course SET Student_Name = '" + tbStudentName.Text + "' WHERE Course_ID = '" + regNo + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[3].Value.ToString() != tbTeacherName.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Course SET Teacher_Name = '" + tbTeacherName.Text + "' WHERE Course_ID = '" + regNo + "'", con))
                        {
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid input.");
                    }
                }
                if (dtvTable.Rows[rowIndex].Cells[4].Value.ToString() != tbSemester.Text)
                {
                    try
                    {
                        var con = Configuration.getInstance().getConnection();
                        using (SqlCommand command = new SqlCommand("UPDATE Course SET Semester = '" + tbSemester.Text + "' WHERE Course_ID = '" + regNo + "'", con))
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
                SqlCommand cmd = new SqlCommand("select * from Course " +
                    "where Course_ID like '%" + txt + "%' " +
                    "or Course_Name like '%" + txt + "%' " +
                    "or Student_Name like '%" + txt + "%' " +
                    "or Teacher_Name like '%" + txt + "%' " +
                    "or Semester like '%" + txt + "%'", con);
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
