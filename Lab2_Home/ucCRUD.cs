using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Home
{
    public partial class ucCRUD : UserControl
    {
        private static ucCRUD _instence;
        private bool isStudentField = false;
        private bool isCourseField = false;
        private bool isCourseRegisterField = false;

        public static ucCRUD Instence
        {
            get
            {
                if (_instence == null)
                {
                    _instence = new ucCRUD();
                }
                return _instence;
            }
        }
        public ucCRUD()
        {
            InitializeComponent();
        }

        private void dtvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (isStudentField == false && isCourseField == false && isCourseRegisterField == true)
                {
                    ucRegisterStudent.Instence.updateIndex_RegNo(e.RowIndex);
                }
                else if (isStudentField == true && isCourseField == false && isCourseRegisterField == false)
                {
                    ucStudent.Instence.updateIndex_RegNo(e.RowIndex);
                }
                else if (isStudentField == false && isCourseField == true && isCourseRegisterField == false)
                {
                    ucCourses.Instence.updateIndex_RegNo(e.RowIndex);
                }
            }
        }

        public DataGridView getDtvTable()
        {
            return dtvTable;
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            isStudentField = true;
            isCourseField = false;
            isCourseRegisterField = false;
            if (!pnlInput.Controls.Contains(ucStudent.Instence))
            {
                pnlInput.Controls.Add(ucStudent.Instence);
                ucStudent.Instence.Dock = DockStyle.Fill;
                panel2.Show();
                ucStudent.Instence.BringToFront();
            }
            else
            {
                ucStudent.Instence.refreshUC();
                ucStudent.Instence.BringToFront();
            }
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            isStudentField = false;
            isCourseField = true;
            isCourseRegisterField = false;
            if (!pnlInput.Controls.Contains(ucCourses.Instence))
            {
                pnlInput.Controls.Add(ucCourses.Instence);
                ucCourses.Instence.Dock = DockStyle.Fill;
                panel2.Show();
                ucCourses.Instence.BringToFront();
            }
            else
            {
                ucCourses.Instence.refreshUC();
                ucCourses.Instence.BringToFront();
            }
        }

        private void btnRegStudent_Click(object sender, EventArgs e)
        {
            isStudentField = false;
            isCourseField = false;
            isCourseRegisterField = true;
            if (!pnlInput.Controls.Contains(ucRegisterStudent.Instence))
            {
                pnlInput.Controls.Add(ucRegisterStudent.Instence);
                ucRegisterStudent.Instence.Dock = DockStyle.Fill;
                panel2.Show();
                ucRegisterStudent.Instence.BringToFront();
            }
            else
            {
                ucRegisterStudent.Instence.refreshUC();
                ucRegisterStudent.Instence.BringToFront();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (isStudentField == false && isCourseField == false && isCourseRegisterField == true)
            {
                ucRegisterStudent.Instence.loadSeachedInDtv(tbSearch.Text);
            }
            else if (isStudentField == true && isCourseField == false && isCourseRegisterField == false)
            {
                ucStudent.Instence.loadSeachedInDtv(tbSearch.Text);
            }
            else if (isStudentField == false && isCourseField == true && isCourseRegisterField == false)
            {
                ucCourses.Instence.loadSeachedInDtv(tbSearch.Text);
            }
        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            Form1.Instence.toAttendenceUC();
        }
    }
}
