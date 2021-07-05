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

namespace PersonalTracking
{
    public partial class FrmEmployeeList : Form
    {
        EmployeeDTO dto = new EmployeeDTO();
        private bool comboFull = false;
        EmployeeDetailDTO detail = new EmployeeDetailDTO();

        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = new FrmEmployee();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.employeeID == 0)
                MessageBox.Show("Please select an employee on table");
            else
            {
                FrmEmployee frm = new FrmEmployee();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanFilters();
            }
        }

        private void FillAllData()
        {
            dto = EmployeeBLL.GetAll();
            dataGridView1.DataSource = dto.employees;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Department";
            dataGridView1.Columns[5].HeaderText = "Position";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Salary";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            comboFull = false;
            cmbDepartment.DataSource = dto.Departements;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
        }

        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            FillAllData();
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == 
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = dto.employees;
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
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters(); 
        }

        private void CleanFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            dataGridView1.DataSource = dto.employees;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.password = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.imagePath = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            detail.adress = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            detail.isAdmin = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.birthDay = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString());
            detail.userNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            detail.departmentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            detail.positionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            detail.employeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            detail.salary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this employee", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                EmployeeBLL.DeleteEmployee(detail.employeeID);
                MessageBox.Show("Employee was deleted");
                FillAllData();
                CleanFilters();
            }
        }
    }
}
