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

namespace PersonalTracking
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //propiedad que indica que se ha controlado o no el evento
            e.Handled = General.isNumber(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "" || txtPassword.Text.Trim() == "")
                MessageBox.Show("Pelase fill the userNo and password");
            else
            {
                List<EMPLOYEE> employeeList = EmployeeBLL.getEmployess(Convert.ToInt32(txtUserNo.Text), txtPassword.Text);
                if (employeeList.Count == 0)
                    MessageBox.Show("Please control your information");
                else
                {
                    EMPLOYEE employee = new EMPLOYEE();
                    employee = employeeList.First();
                    UserStatic.employeeID = employee.ID;
                    UserStatic.userNo = employee.UserNo;
                    UserStatic.isAdmin = Convert.ToBoolean(employee.isAdmin);
                    FrmMain frm = new FrmMain();
                    this.Hide();//Esconde el formulario actual
                    frm.ShowDialog();//Muestra el formulario
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
