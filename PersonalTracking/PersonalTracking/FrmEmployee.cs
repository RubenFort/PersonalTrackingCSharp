using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate = false;
        string imagePath = "";

        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        //Recojer valores de Departments y Positions de la BD y mostrarlos en los ComboBox del formulario
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();//OJO
            cmbDepartment.DataSource = dto.Departements;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            if (isUpdate)
            {
                txtName.Text = detail.name;
                txtSurname.Text = detail.surname;
                txtUserNo.Text = detail.userNo.ToString();
                txtPassword.Text = detail.password;
                chAdmin.Checked = Convert.ToBoolean(detail.isAdmin);
                txtAdress.Text = detail.adress;
                dtBirthday.Value = Convert.ToDateTime(detail.birthDay);
                cmbDepartment.SelectedValue = detail.departmentID;
                cmbPosition.SelectedValue = detail.positionID;
                txtSalary.Text = detail.salary.ToString();
                imagePath = Application.StartupPath + "\\images\\" + detail.imagePath;
                txtImagePath.Text = imagePath;
                pictureBox1.ImageLocation = imagePath;
            }
        }
        bool comboFull = false;
        //Mostrar los puestos de trabajo asociados a un departmento en concreto
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
        }

        string fileName = "";
        //Cargar Imagen
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                //GUID representa un identificador único global 
                string unique = Guid.NewGuid().ToString();
                fileName += unique + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User no is Empty");
            else if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                MessageBox.Show("This user No is used by another employee, please change");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password is Empty");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name is Empty");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname is Empty");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary is Empty");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Select a Department");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Select a Position");
            else
            {
                EMPLOYEE employee = new EMPLOYEE();
                //MessageBox.Show(label.Text);
                employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                employee.Password = txtPassword.Text;
                employee.isAdmin = chAdmin.Checked;
                employee.Name = txtName.Text;
                employee.SurName = txtSurname.Text;
                employee.Salary = Convert.ToInt32(txtSalary.Text);
                employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                employee.Adress = txtAdress.Text;
                employee.BirthDay = dtBirthday.Value;
                employee.ImagePath = fileName;
                EmployeeBLL.AddEmployee(employee);
                File.Copy(txtImagePath.Text, @"images\\" + fileName);//Copiar imagen a capeta(persistir datos)
                MessageBox.Show("Employee was Added");
                resertForm();
            }
        }

        private void resertForm()
        {
            txtUserNo.Clear();
            txtPassword.Clear();
            chAdmin.Checked = false;
            txtName.Clear();
            txtSurname.Clear();
            txtSalary.Clear();
            txtAdress.Clear();
            txtImagePath.Clear();
            pictureBox1.Image = null;
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            dtBirthday.Value = DateTime.Today;
        }

        bool isUnique = false;
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User no is Empty");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                    MessageBox.Show("This user No is used by another employee, please change");
                else
                    MessageBox.Show("This user is usable");
            }
        }
    }
}
