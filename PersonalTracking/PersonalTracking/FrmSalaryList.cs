using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.DTO;
using BLL;
using DAL;

namespace PersonalTracking
{
    public partial class FrmSalaryList : Form
    {
        public FrmSalaryList()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmSalary frm = new FrmSalary();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            clenFilter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmSalary frm = new FrmSalary();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SalaryDTO dto = new SalaryDTO();
        private bool comboFull;
        void FillAllData()
        {
            dto = SalaryBLL.GetAll();
            //Si el usuario no es Admin solo se muestra su salario, no tiene acceso al salario del resto de empleados
            if (!UserStatic.isAdmin)
                dto.salaries = dto.salaries.Where(x => x.employeeID == UserStatic.employeeID).ToList();  
            dataGridView1.DataSource = dto.salaries;
            //Lógica de limpieza
            comboFull = false;
            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            if (dto.departments.Count > 0)
                comboFull = true;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbMonth.DataSource = dto.months;
            cmbMonth.DisplayMember = "Month Name";
            cmbMonth.ValueMember = "MonthName";
            cmbMonth.SelectedIndex = -1;
        }

        private void FrmSalaryList_Load(object sender, EventArgs e)
        {
            FillAllData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Month";
            dataGridView1.Columns[9].HeaderText = "Year";
            dataGridView1.Columns[11].HeaderText = "Salary";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            if (!UserStatic.isAdmin)
            {
                btnUpdate.Hide();
                btnDelete.Hide();
                btnNew.Location = new Point(417, 20);
                btnClose.Location = new Point(575, 20);
                pnlForAdmin.Hide();
            }
            
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalaryDetailDTO> list = dto.salaries;
            if (txtUserNo.Text.Trim() != "")
                list = list.Where(x => x.userNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                list = list.Where(x => x.name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.surname.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (txtYear.Text.Trim() != "")
                list = list.Where(x => x.salaryYear == Convert.ToInt32(txtSalary.Text)).ToList();
            if (cmbMonth.SelectedIndex != -1)
                list = list.Where(x => x.monthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            if (txtSalary.Text.Trim() != "")
            {
                if (rbMore.Checked)
                    list = list.Where(x => x.salaryAmount > Convert.ToInt32(txtSalary.Text)).ToList();
                else if (rbLess.Checked)
                    list = list.Where(x => x.salaryAmount < Convert.ToInt32(txtSalary.Text)).ToList();
                else
                    list = list.Where(x => x.salaryAmount == Convert.ToInt32(txtSalary.Text)).ToList();
            }
                
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            clenFilter();
        }

        private void clenFilter()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            cmbMonth.SelectedIndex = -1;
            rbMore.Checked = false;
            rbLess.Checked = false;
            rbEquals.Checked = false;
            txtYear.Clear();
            txtSalary.Clear();
            dataGridView1.DataSource = dto.salaries;
        }
    }
}
