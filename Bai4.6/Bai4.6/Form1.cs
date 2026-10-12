using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4._6
{
    public partial class Form1 : Form
    {
        private BindingList<Student> students = new BindingList<Student>();
        private BindingSource bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();

            bindingSource.DataSource = students;
            dataGridView1.DataSource = bindingSource;

            txtStudentId.DataBindings.Add(
                "Text",
                bindingSource,
                "StudentId",
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtFullName.DataBindings.Add(
                "Text",
                bindingSource,
                "FullName",
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtAge.DataBindings.Add(
                "Text",
                bindingSource,
                "Age",
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            cboGender.DataBindings.Add(
                "Text",
                bindingSource,
                "Gender",
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            bindingSource.CurrentChanged += BindingSource_CurrentChanged;
        }
        private void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingSource.Current is Student student)
            {
                if (!string.IsNullOrEmpty(student.AvatarPath)
                    && File.Exists(student.AvatarPath))
                {
                    picAvatar.ImageLocation = student.AvatarPath;
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra mã sinh viên
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống!");
                txtStudentId.Focus();
                return;
            }

            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ tên không được để trống!");
                txtFullName.Focus();
                return;
            }

            // Kiểm tra tuổi
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Tuổi phải là số!");
                txtAge.Focus();
                return;
            }

            if (age < 1 || age > 100)
            {
                MessageBox.Show("Tuổi phải nằm trong khoảng từ 1 đến 100!");
                txtAge.Focus();
                return;
            }

            // Kiểm tra giới tính
            if (string.IsNullOrWhiteSpace(cboGender.Text))
            {
                MessageBox.Show("Bạn chưa chọn giới tính!");
                cboGender.Focus();
                return;
            }

            // Kiểm tra trùng mã sinh viên
            foreach (Student s in students)
            {
                if (s.StudentId.Equals(
                    txtStudentId.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!");
                    txtStudentId.Focus();
                    return;
                }
            }

            // Nếu tất cả hợp lệ thì thêm
            Student student = new Student
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Age = age,
                Gender = cboGender.Text,
                AvatarPath = ""
            };

            students.Add(student);

            MessageBox.Show("Thêm sinh viên thành công!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current != null)
            {
                bindingSource.RemoveCurrent();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bindingSource.EndEdit();
            dataGridView1.Refresh();

            MessageBox.Show("Cập nhật thành công!");
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is Student student)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    student.AvatarPath = openFileDialog.FileName;

                    picAvatar.ImageLocation = student.AvatarPath;

                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn sinh viên trước!");
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CSV file|*.csv";
            saveFileDialog.FileName = "students.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(
                    saveFileDialog.FileName,
                    false,
                    Encoding.UTF8))
                {
                    writer.WriteLine(
                        "StudentId,FullName,Age,Gender,AvatarPath");

                    foreach (Student student in students)
                    {
                        writer.WriteLine(
                            $"{student.StudentId}," +
                            $"{student.FullName}," +
                            $"{student.Age}," +
                            $"{student.Gender}," +
                            $"{student.AvatarPath}");
                    }
                }

                MessageBox.Show("Xuất CSV thành công!");
            }
        }

        private void txtFullName_Click(object sender, EventArgs e)
        {

        }

        private void picAvatar_Click(object sender, EventArgs e)
        {

        }

        private void sa_Load(object sender, EventArgs e)
        {

        }
    }
}
