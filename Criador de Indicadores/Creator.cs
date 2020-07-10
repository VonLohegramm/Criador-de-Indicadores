using System;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Text;

namespace Criador_de_Indicadores
{
    public partial class Creator : Form
    {
        public Creator()
        {
            InitializeComponent();
        }

        string defaultPath = @ConfigurationManager.AppSettings["defaultPath"];  //@"F:\Área de Trabalho\Indicador";
        string filePath = @ConfigurationManager.AppSettings["filePath"]; //"F:\Área de Trabalho";
        string fileName = @ConfigurationManager.AppSettings["fileName"]; //"excel.xlsx";

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGerar_click(object sender, EventArgs e)
        {

            if (cmbAno.Text == "" || cmbMes.Text == "")
            {
                MessageBox.Show("Selecione um ano e um mês", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ano = cmbAno.Text;
                DirectoryInfo diAno = Directory.CreateDirectory(defaultPath + $@"\{ano}");

                string mes = cmbMes.Text;

                if (Directory.Exists(defaultPath + $@"\{ano}" + $@"\{mes}"))
                {
                    MessageBox.Show($"Mês de {mes} já foi criado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(!File.Exists(Path.Combine(filePath, fileName)))
                {
                    MessageBox.Show("Planilha modelo não existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                 DirectoryInfo diMes = Directory.CreateDirectory(defaultPath + $@"\{ano}" + $@"\{mes}");

                string targetPath = defaultPath + $@"\{ano}" + $@"\{mes}";

                string sourceFile = Path.Combine(filePath, fileName);
                string destFile = Path.Combine(targetPath, fileName);

                int month = 0;

                switch(mes)
                {
                    case "Janeiro":
                        month = 1;
                        break;
                    case "Fevereiro":
                        month = 2;
                        break;
                    case "Março":
                        month = 3;
                        break;
                    case "Abril":
                        month = 4;
                        break;
                    case "Maio":
                        month = 5;
                        break;
                    case "Junho":
                        month = 6;
                        break;
                    case "Julho":
                        month = 7;
                        break;
                    case "Agosto":
                        month = 8;
                        break;
                    case "Setembro":
                        month = 9;
                        break;
                    case "Outubro":
                        month = 10;
                        break;
                    case "Novembro":
                        month = 11;
                        break;
                    case "Dezembro":
                        month = 12;
                        break;
                }

                int totalDias = DateTime.DaysInMonth(int.Parse(ano),  month);

                for(int i = 1; i <= totalDias; i++)
                {
                    File.Copy(sourceFile, destFile);
                    FileInfo file2 = new FileInfo(destFile);
                    StringBuilder newName = new StringBuilder($@"{targetPath}\{i} {mes.ToUpper()}.xlsx");

                    //string newName = $@"{targetPath}\{i} {mes.ToUpper()}.xlsx";
                    file2.MoveTo(newName.ToString());
                }

                MessageBox.Show("Arquivos gerados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
