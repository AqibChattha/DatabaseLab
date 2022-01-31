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

namespace Lab2_Home
{
    public partial class Form1 : Form
    {
        private static Form1 _instence;
        public static Form1 Instence
        {
            get
            {
                if (_instence == null)
                {
                    _instence = new Form1();
                }
                return _instence;
            }
        }

        private Form1()
        {
            InitializeComponent();
            toCRUD_UC();
        }

        public void toAttendenceUC()
        {
            if (!panel1.Controls.Contains(ucAttendence.Instence))
            {
                panel1.Controls.Add(ucAttendence.Instence);
                ucAttendence.Instence.Dock = DockStyle.Fill;
                ucAttendence.Instence.BringToFront();
            }
            else
            {
                ucAttendence.Instence.refreshUC();
                ucAttendence.Instence.BringToFront();
            }
        }
        public void toCRUD_UC()
        {
            if (!panel1.Controls.Contains(ucCRUD.Instence))
            {
                panel1.Controls.Add(ucCRUD.Instence);
                ucCRUD.Instence.Dock = DockStyle.Fill;
                ucCRUD.Instence.BringToFront();
            }
            else
            {
                //ucCRUD.Instence.refreshUC();
                ucCRUD.Instence.BringToFront();
            }
        }
    }
}
