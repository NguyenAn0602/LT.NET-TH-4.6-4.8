using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4._7
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        public void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }
        private void OpenChildForm(Form childForm)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == childForm.GetType())
                {
                    form.Activate();
                    childForm.Dispose();
                    return;
                }
            }

            childForm.MdiParent = this;
            childForm.Show();
        }
        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void quảnLýSảnPhảmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProduct());
        }
        private void lblStatus_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormStudent());
        }

        private void casecadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã đăng xuất!");
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormStudent());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProduct());
        }
    }
}
