using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        TimeSpan permissionDay;//Para calcular días de vacaiones del usuario
        public bool isUpdate = false;
        public PermissionDetailsDTO detail = new PermissionDetailsDTO();

        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.userNo.ToString();
            if (isUpdate)
            {
                dpStart.Value = detail.startDate.Value;
                dpEnd.Value = detail.endDate.Value;
                txtDayAmount.Text = detail.permissionDayAmount.ToString();
                txtExplanation.Text = detail.explanation;
                txtUserNo.Text = detail.userNo.ToString();
            }
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dpEnd.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString();
        }

        private void dpEnd_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dpEnd.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("Please change end or start date");
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("Permission  day must be bigger tha 0");
            else if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("Explanation is empty");
            else
            {
                PERMISSION permission = new PERMISSION();
                permission.EmployeeID = UserStatic.employeeID;
                permission.PermissionState = 1;
                permission.PermissionStartDate = dpStart.Value.Date;
                permission.PermissionEndDate = dpEnd.Value.Date;
                permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                permission.PermissionExplanation = txtExplanation.Text;
                PermissionBLL.AddPermisssion(permission);
                MessageBox.Show("Permision was adeed");
                permission = new PERMISSION();

                dpStart.Value = DateTime.Today;
                dpEnd.Value = DateTime.Today;
                txtDayAmount.Clear();
                txtExplanation.Clear();
            }
        }
    }
}
