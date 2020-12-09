using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Close();
        }

        private void sociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioSocios fSocios = new FormularioSocios();
            fSocios.ShowDialog();
        }

        private void pistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioPistas fpistas = new FormularioPistas();
            fpistas.ShowDialog();
        }

        private void rESERVASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioReservas fReservas = new FormularioReservas();
            fReservas.MdiParent = this;
            fReservas.Dock = DockStyle.Fill;
            fReservas.Show();
        }
    }
}
