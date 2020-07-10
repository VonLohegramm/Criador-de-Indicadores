using System;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace Criador_de_Indicadores
{
    public partial class Calculate : Form
    {
        public Calculate()
        {
            InitializeComponent();
        }
        private static void CloseExcel(Microsoft.Office.Interop.Excel.Application ExcelApplication = null)
        {
            if (ExcelApplication != null)
            {
                ExcelApplication.Workbooks.Close();
                ExcelApplication.Quit();
            }

            System.Diagnostics.Process[] PROC = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process PK in PROC)
            {
                if (PK.MainWindowTitle.Length == 0) { PK.Kill(); }
            }
        }


        string defaultPath = @ConfigurationManager.AppSettings["defaultPath"];  //@"F:\Área de Trabalho\Indicador";


        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            if (cmbAno.Text == "" || cmbMes.Text == "")
            {
                MessageBox.Show("Selecione um ano e um mês", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ano = cmbAno.Text;

                string mes = cmbMes.Text;

                string[] excelPath = Directory.GetFiles(defaultPath + $@"\{ano}" + $@"\{mes}");
                int value = 0;

                if (excelPath.Length != 0 && !(excelPath is null))
                {
                    foreach (string path in excelPath)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook excel = app.Workbooks.Open(path);
                        Worksheet excelSheet = excel.ActiveSheet;
                        Console.WriteLine(path);
                        value += int.Parse(excelSheet.Cells[13, 4].Value.ToString());

                        CloseExcel(app);
                    }
                }
                else
                {
                    MessageBox.Show("Não existe nenhuma planilha para realizar o cálculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Total de não conformidade: {value}","Total", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
