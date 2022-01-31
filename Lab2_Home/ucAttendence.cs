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
    public partial class ucAttendence : UserControl
    {
        private static ucAttendence _instence;
        private bool ColomnAdded = false;
        public static ucAttendence Instence
        {
            get
            {
                if (_instence == null)
                {
                    _instence = new ucAttendence();
                }
                return _instence;
            }
        }

        public ucAttendence()
        {
            InitializeComponent();
            loadDataInCourseDTV();
            label3.Text = "Date: " + DateTime.Today.ToShortDateString();
        }

        public void refreshUC()
        {

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Form1.Instence.toCRUD_UC();
        }

        private void loadDataInCourseDTV(string courseID="")
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select Course_Name from Course WHERE Course_Name LIKE '%" + courseID + "%' or Course_ID LIKE '%" + courseID + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtvCourses.DataSource = dt;

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input.");
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            loadDataInCourseDTV(tbSearch.Text);
        }

        private void dtvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string courseText = dtvCourses.Rows[e.RowIndex].Cells[0].Value.ToString();
                lbtitle.Text = ("Take Attendence for course:\n"+ courseText);
                viewDtvAttendence(courseText);
                
            }
        }
        private void viewDtvAttendence(string courseText)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select StudentRegNo, CourseName from Enrollments WHERE CourseName = '" + courseText + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtvAttendence.DataSource = dt;
                if (ColomnAdded == false)
                {
                    var col3 = new DataGridViewCheckBoxColumn();
                    col3.HeaderText = "is present";
                    col3.Name = "Status";
                    dtvAttendence.Columns.AddRange(new DataGridViewColumn[] { col3 });
                    ColomnAdded = true;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                string regNo = "", name = "";
                int status = 0;
                for (int i = 0; i < dtvAttendence.Rows.Count; i++)
                {
                    regNo = dtvAttendence.Rows[i].Cells["StudentRegNo"].Value.ToString();
                    name = dtvAttendence.Rows[i].Cells["CourseName"].Value.ToString();
                    if (Convert.ToBoolean(dtvAttendence.Rows[i].Cells["Status"].Value))
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }
                    SqlCommand cmd = new SqlCommand("Insert into Attendance values (@StudentRegNo, @CourseName, @TimeStamp, @Status)", con);
                    cmd.Parameters.AddWithValue("@StudentRegNo", regNo);
                    cmd.Parameters.AddWithValue("@CourseName", name);
                    cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Attendance Saved");
                con.Close();
                dtvAttendence.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }

        }
    }
}
