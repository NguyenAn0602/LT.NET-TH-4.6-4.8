using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4._8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async Task<List<Student>> LoadStudentsAsync(IProgress<int> progress)
        {
            return await Task.Run(() =>
            {
                List<Student> students = new List<Student>();

                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(50);

                    students.Add(new Student
                    {
                        StudentId = "SV" + i.ToString("000"),
                        FullName = "Sinh viên " + i,
                        Age = 18 + i % 5,
                        Gender = i % 2 == 0 ? "Nam" : "Nữ"
                    });

                    progress.Report(i);
                }

                return students;
            });
        }

        private async void btnLoadData_Click(object sender, EventArgs e)
        {
            btnLoadData.Enabled = false;

            lblStatus.Text = "Đang chuẩn bị nạp dữ liệu...";
            
            progressBar1.Value = 0;
            lblProgress.Text = "0%";

            try
            {
                Progress<int> progress = new Progress<int>(value =>
                {
                    progressBar1.Value = value;
                    lblProgress.Text = value + "%";
                   
                    lblStatus.Text = "Đang nạp dữ liệu: " + value + "%";
                });

                List<Student> students = await LoadStudentsAsync(progress);

                dataGridView1.DataSource = students;

                lblStatus.Text = "Nạp dữ liệu hoàn tất";

                MessageBox.Show("Nạp dữ liệu thành công!");
            }
            catch (Exception ex)
            {

                lblStatus.Text = "Nạp dữ liệu thất bại";

                MessageBox.Show("Có lỗi: " + ex.Message);
            }
            finally
            {
                btnLoadData.Enabled = true;
            }
        }
    }
}
