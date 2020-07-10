using System;
using System.Windows.Forms;

namespace Criador_de_Indicadores
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnOpenCreator(object sender, EventArgs e)
        {
            Creator creator = new Creator();
            creator.Show();
        }

        private void btnOpenCalculater(object sender, EventArgs e)
        {
            Calculate calculate = new Calculate();
            calculate.Show();
        }
    }
}
