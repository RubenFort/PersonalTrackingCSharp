using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTracking
{
    public class ExportToExcel
    {
        internal static void ExcelExport(DataGridView dataGridView1)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //Crear libro de trabajo
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            //Crear hoja del libro de trabajo(cada una de las página de los excel)
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            //Formatea cabecera del Excell, en excell las celdas empiezan en 1
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                try
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                catch (Exception)
                {

                }
            }
            //Rellenar contenido del Excel
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    try
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    catch (Exception)
                    {
                        
                    }
                    try
                    {
                        workbook.SaveAs("C:\\ourexcell.xlsl");
                        workbook.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
