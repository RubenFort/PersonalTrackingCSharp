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
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void textUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            fillAllDate();
            cleanFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.permissionID == 0)
                MessageBox.Show("Please select a parmission from table");
            else
            {
                FrmPermission frmPermission = new FrmPermission();
                frmPermission.isUpdate = true;
                frmPermission.detail = detail;
                this.Hide();
                frmPermission.ShowDialog();
                this.Visible = true;
                fillAllDate();
                cleanFilters();
            }
        }

        PermissionDTO dto = new PermissionDTO();
        private bool comboFull;

        void fillAllDate()
        {
            dto = PermissionBLL.getAll();
            dataGridView1.DataSource = dto.permissions;

            comboFull = false;
            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbState.DataSource = dto.states;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            comboFull = true;
        }

        private void FrmPermissionList_Load_1(object sender, EventArgs e)
        {
            fillAllDate();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Start Date";
            dataGridView1.Columns[9].HeaderText = "End Date";
            dataGridView1.Columns[10].HeaderText = "Day Amount";
            dataGridView1.Columns[11].HeaderText = "State";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailsDTO> list = dto.permissions;
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
            if (rbStart.Checked)
                list = list.Where(x => x.startDate > Convert.ToDateTime(dpStart.Value) &&
                x.startDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if (rbEndDate.Checked)
                list = list.Where(x => x.endDate > Convert.ToDateTime(dpStart.Value) &&
                x.endDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if (cmbState.SelectedIndex != -1)
                list = list.Where(x => x.state == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (txtDayAmount.Text.Trim() != "")
                list = list.Where(x => x.permissionDayAmount == Convert.ToInt32(txtDayAmount.Text)).ToList();

            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanFilters();
        }

        private void cleanFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            rbEndDate.Checked = false;
            rbStart.Checked = false;
            cmbState.SelectedIndex = -1;
            dataGridView1.DataSource = dto.permissions;
        }
        PermissionDetailsDTO detail = new PermissionDetailsDTO();

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.permissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.startDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.endDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.explanation = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.userNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.state = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.permissionDayAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermisssion(detail.permissionID, PermissionState.approved);
            MessageBox.Show("Approved");
            fillAllDate();
            cleanFilters();
        }

        private void btnDisapprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermisssion(detail.permissionID, PermissionState.disapproved);
            MessageBox.Show("Disapproved");
            fillAllDate();
            cleanFilters();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("are you sure to delete this permission", "Warning", MessageBoxButtons.YesNo);
        }
    }
}
