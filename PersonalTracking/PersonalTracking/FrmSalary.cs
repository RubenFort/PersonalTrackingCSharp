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
using DAL;
using BLL;

namespace PersonalTracking
{
    public partial class FrmSalary : Form
    {
        SALARY salary = new SALARY();
        int oldSalary = 0;

        public FrmSalary()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Trim() == "")
                MessageBox.Show("Please fill the year");
            else if (cmbMonth.SelectedIndex == -1)
                MessageBox.Show("Please select a month");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Please fill the salary");
            else
            {
                bool control = false;
                if (!isUpdate)
                {
                    if (salary.EmployeeID == 0)
                        MessageBox.Show("Please select an employee from the table");
                    else
                    {
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(txtSalary.Text);
                        if (salary.Amount > detail.oldSalary)
                            control = true;
                        SalaryBLL.AddSalary(salary, control);
                        MessageBox.Show("Salary was added");
                        cmbMonth.SelectedIndex = -1;
                        salary = new SALARY();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Title", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == result)
                    {
                        SALARY salary = new SALARY();
                        salary.ID = detail.salaryID;
                        salary.EmployeeID = detail.employeeID;
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(txtSalary.Text);
                        if (salary.Amount > detail.oldSalary)
                            control = true;
                        SalaryBLL.UpdateSalary(salary, control);
                        MessageBox.Show("Salary was updated");
                        this.Close();
                    }
                }
            }
        }

        SalaryDTO dto = new SalaryDTO();
        private bool comboFull;
        public SalaryDetailDTO detail = new SalaryDetailDTO();
        public bool isUpdate = false;

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                dto = SalaryBLL.GetAll();
                dataGridView1.DataSource = dto.employees;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "User No";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;

                comboFull = false;
                cmbDepartment.DataSource = dto.departments;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "ID";
                cmbPosition.DataSource = dto.positions;
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.ValueMember = "ID";
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.SelectedIndex = -1;

                if (dto.departments.Count > 0)
                    comboFull = true;
            }

            cmbMonth.DataSource = dto.months;//Datos para el ComboBox
            cmbMonth.DisplayMember = "MonthName";//Columna que selecciono para mostrar datos en ComboBox
            cmbMonth.ValueMember = "ID";//Columna que selecciono para coger los datos seleccionados en ComboBox
            cmbMonth.SelectedIndex = -1;

            if (isUpdate)
            {
                panel1.Hide();
                txtName.Text = detail.name;
                txtSalary.Text = detail.salaryAmount.ToString();
                txtSurname.Text = detail.surname;
                txtYear.Text = detail.salaryYear.ToString();
                //cmbMonth.SelectedValue = detail.monthID;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYear.Text = DateTime.Today.Year.ToString();
            txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            salary.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            oldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
        }
    }
}
