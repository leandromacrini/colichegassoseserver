using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushNotificationServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.Activate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!realExit)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void openServerConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }


        private bool realExit = false;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            realExit = true;
            Application.Exit();
        }
    }
}
