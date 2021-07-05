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
    public partial class FrmDepartmentList : Form
    {
        //En este caso el DTO, es DEPARTMENT, es un objeto de directamente de la BD
        public DEPARTMENT detail = new DEPARTMENT();
        List<DEPARTMENT> list = new List<DEPARTMENT>();

        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmDepartment frm = new FrmDepartment();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            list = DepartmentBLL.GetDepartments();
            dataGridDepartment.DataSource = list;//Muestra los nuevos resultados al volver de FrmDepartment
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
                MessageBox.Show("Please select a departement from table");
            else
            {
                FrmDepartment frm = new FrmDepartment();
                frm.isUpdate = true;
                frm.detail = detail;//Valores recogidos de DataGridView
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                list = DepartmentBLL.GetDepartments();
                dataGridDepartment.DataSource = list;
            }
        }

        /// <summary>
        /// Mostrar listado de departamentos en una tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {
            list = DepartmentBLL.GetDepartments();
            dataGridDepartment.DataSource = list;//Mostrar datos
            //dataGridDepartment.Columns[0].Visible = false;//Oculta columna 0
            dataGridDepartment.Columns[0].HeaderText = "Department ID";//Al mostrar cambiar nombre columna 
            dataGridDepartment.Columns[1].HeaderText = "Department Name";
        }

        private void dataGridDepartment_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ID = Convert.ToInt32(dataGridDepartment.Rows[e.RowIndex].Cells[0].Value);
            detail.DepartmentName = dataGridDepartment.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this Position", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result)
            {
                DepartmentBLL.DeleteDepartment(detail.ID);
                MessageBox.Show("Employee was deleted");
                list = DepartmentBLL.GetDepartments();
                dataGridDepartment.DataSource = list;
            }
        }
    }
}
