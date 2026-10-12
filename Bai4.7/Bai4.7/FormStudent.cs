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
    public partial class FormStudent : Form
    {
        public FormStudent()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormMain main)
            {
                main.UpdateStatus("Đã thêm sinh viên thành công");
            }

            MessageBox.Show("Thêm sinh viên thành công!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void xóaDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                if (this.MdiParent is FormMain main)
                {
                    main.UpdateStatus("Đã xóa một dòng");
                }
            }
        }

        private void xemChiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                MessageBox.Show("Đang xem chi tiết dòng được chọn");
            }
        }
    }
}
